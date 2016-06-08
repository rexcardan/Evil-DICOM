﻿using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.PDUs.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Extensions
{
    /// <summary>
    /// Class AssociationExtensions.
    /// </summary>
    public static class AssociationExtensions
    {
        /// <summary>
        /// Takes two sets of presentation contexts (typically from a request and a DICOM Service) and creates the
        /// agreed presentation context list for the accept response
        /// </summary>
        /// <param name="asc">The asc.</param>
        /// <param name="reqContexts">The req contexts.</param>
        /// <returns>List&lt;PresentationContext&gt;.</returns>
        public static List<PresentationContext> GetResponseContexts(this Association asc, IEnumerable<PresentationContext> reqContexts)
        {
            var serviceContexts = asc.PresentationContexts;
            List<PresentationContext> response = new List<PresentationContext>();

            foreach (var ctx in reqContexts)
            {
                var supported = serviceContexts.FirstOrDefault(c => c.AbstractSyntax == ctx.AbstractSyntax);
                var abSyntaxSupported = supported != null;

                if (abSyntaxSupported)
                {
                    var agreedTxSyntax = ctx.TransferSyntaxes
                        .Where(t => !string.IsNullOrEmpty(t))
                        .Intersect(supported.TransferSyntaxes)
                        .ToList();

                    if (agreedTxSyntax.Any())
                    {
                        //All good - Abstract syntax and at least one transfer syntax are supported
                        ctx.Reason = Enums.PresentationContextReason.ACCEPTANCE;
                        ctx.TransferSyntaxes.Clear();
                        ctx.TransferSyntaxes.Add(agreedTxSyntax.First());
                        response.Add(ctx);
                    }
                    else
                    {
                        //Transfer syntax not supported
                        asc.Logger.Log("Transfer syntax(es) not supported : {0}", string.Join(",", ctx.TransferSyntaxes));
                        ctx.Reason = Enums.PresentationContextReason.TRANSFER_SYNAXES_NOT_SUPPORTED;
                        response.Add(ctx);
                    }
                }
                else
                {
                    //Abstract Syntax not supported
                    asc.Logger.Log("Abstract syntax not supported : {0}", string.Join(",", ctx.TransferSyntaxes));
                    ctx.Reason = Enums.PresentationContextReason.ABSTRACT_SYNAX_NOT_SUPPORTED;
                    var syntax = ctx.TransferSyntaxes.First();
                    ctx.TransferSyntaxes.Clear();
                    ctx.TransferSyntaxes.Add(syntax);
                    response.Add(ctx);
                }

            }
            return response;
        }

        /// <summary>
        /// Sets the final contexts.
        /// </summary>
        /// <param name="asc">The asc.</param>
        /// <param name="accept">The accept.</param>
        public static void SetFinalContexts(this Association asc, Accept accept)
        {
            List<PresentationContext> final = new List<PresentationContext>();
            foreach (var ctx in asc.PresentationContexts)
            {
                var accepted = accept.PresentationContexts.FirstOrDefault(p => p.Id == ctx.Id);
                if (accepted != null && accepted.Reason == Enums.PresentationContextReason.ACCEPTANCE)
                {
                    ctx.TransferSyntaxes.Clear();
                    ctx.TransferSyntaxes.Add(accepted.TransferSyntaxes.First());
                    final.Add(ctx);
                }
            }
            asc.PresentationContexts = final;
        }
    }
}
