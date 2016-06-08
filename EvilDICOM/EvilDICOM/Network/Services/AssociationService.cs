using System;
using System.Linq;
using EvilDICOM.Network.Associations.PDUs;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Messaging;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.Helpers;
using EvilDICOM.Network.Extensions;

namespace EvilDICOM.Network.Services
{
    /// <summary>
    /// Class AssociationService.
    /// </summary>
    public class AssociationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssociationService"/> class.
        /// </summary>
        public AssociationService()
        {
            SetDefaultActions();
        }

        /// <summary>
        /// Gets or sets the abort received action.
        /// </summary>
        /// <value>The abort received action.</value>
        public Action<Abort, Association> AbortReceivedAction { get; set; }
        /// <summary>
        /// Gets or sets the association request received action.
        /// </summary>
        /// <value>The association request received action.</value>
        public Action<Request, Association> AssociationRequestReceivedAction { get; set; }
        /// <summary>
        /// Gets or sets the association acceptance received action.
        /// </summary>
        /// <value>The association acceptance received action.</value>
        public Action<Accept, Association> AssociationAcceptanceReceivedAction { get; set; }
        /// <summary>
        /// Gets or sets the release request received action.
        /// </summary>
        /// <value>The release request received action.</value>
        public Action<ReleaseRequest, Association> ReleaseRequestReceivedAction { get; set; }
        /// <summary>
        /// Gets or sets the release response action.
        /// </summary>
        /// <value>The release response action.</value>
        public Action<ReleaseResponse, Association> ReleaseResponseAction { get; set; }
        /// <summary>
        /// Gets or sets the association reject action.
        /// </summary>
        /// <value>The association reject action.</value>
        public Action<Reject, Association> AssociationRejectAction { get; set; }

        /// <summary>
        /// Sets the default actions.
        /// </summary>
        public void SetDefaultActions()
        {
            AbortReceivedAction = (abort, asc) =>
            {
                asc.Logger.Log("<-- " + abort);
                RaiseAbortRequestReceived(abort, asc);
                asc.Release();
            };

            AssociationRequestReceivedAction = (req, asc) =>
            {
                asc.Logger.Log("<-- " + req);
                RaiseAssociationRequestReceived(req, asc);
                var ctxs = asc.GetResponseContexts(req.PresentationContexts);
                if (ctxs.Any())
                {
                    var accept = Accept.Generate(req, ctxs);
                    asc.UserInfo = req.UserInfo;
                    asc.State = NetworkState.ASSOCIATION_ESTABLISHED_WAITING_ON_DATA;
                    asc.LastActive = DateTime.Now;
                    asc.PresentationContexts = ctxs; //Simplified agreed contexts
                    AssociationMessenger.SendAccept(accept, asc);
                }
                else
                {
                    asc.State = NetworkState.CLOSING_ASSOCIATION;
                    asc.LastActive = DateTime.Now;
                    AssociationMessenger.SendReject(asc);
                }
            };

            AssociationAcceptanceReceivedAction = (acc, asc) =>
            {
                asc.Logger.Log("<-- " + acc);
                RaiseAssociationAcceptanceReceived(acc, asc);
                asc.SetFinalContexts(acc);
                if (asc.PresentationContexts.Any())
                {
                    asc.UserInfo = acc.UserInfo;

                    asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
                    while (asc.OutboundMessages.Any())
                    {
                        if (asc.State == NetworkState.TRANSPORT_CONNECTION_OPEN)
                        {
                            AbstractDIMSEBase dimse;
                            if (asc.OutboundMessages.TryDequeue(out dimse))
                            {
                                PDataMessenger.Send(dimse, asc);
                            }
                        }
                    }
                }
                else
                {
                    asc.Release();
                }              
            };

            AssociationRejectAction = (rej, asc) =>
            {
                asc.Logger.Log("<-- " + rej);
                RaiseAssociationRejectionReceived(rej, asc);
                asc.Release();
            };

            ReleaseRequestReceivedAction = (rel, asc) =>
            {
                asc.Logger.Log("<-- " + rel);
                RaiseReleaseRequestReceived(rel, asc);
                AssociationMessenger.SendReleaseResponse(asc);
                asc.Release();
            };

            ReleaseResponseAction = (rel, asc) =>
            {
                asc.Logger.Log("<-- " + rel);
                RaiseReleaseResponseReceived(rel, asc);
                asc.Release();
            };
        }

        #region EVENTS AND HANDLERS
        /// <summary>
        /// Delegate AbortRequestHandler
        /// </summary>
        /// <param name="abort">The abort.</param>
        /// <param name="asc">The asc.</param>
        public delegate void AbortRequestHandler(Abort abort, Association asc);

        /// <summary>
        /// Delegate AssociationAcceptedHandler
        /// </summary>
        /// <param name="acc">The acc.</param>
        /// <param name="asc">The asc.</param>
        public delegate void AssociationAcceptedHandler(Accept acc, Association asc);

        /// <summary>
        /// Delegate AssociationRejectedHandler
        /// </summary>
        /// <param name="rej">The rej.</param>
        /// <param name="asc">The asc.</param>
        public delegate void AssociationRejectedHandler(Reject rej, Association asc);

        /// <summary>
        /// Delegate AssociationRequestHandler
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="asc">The asc.</param>
        public delegate void AssociationRequestHandler(Request req, Association asc);

        /// <summary>
        /// Delegate ReleaseRequestHandler
        /// </summary>
        /// <param name="relRq">The relative rq.</param>
        /// <param name="asc">The asc.</param>
        public delegate void ReleaseRequestHandler(ReleaseRequest relRq, Association asc);

        /// <summary>
        /// Delegate ReleaseResponseHandler
        /// </summary>
        /// <param name="relRs">The relative rs.</param>
        /// <param name="asc">The asc.</param>
        public delegate void ReleaseResponseHandler(ReleaseResponse relRs, Association asc);

        //ASSOCIATE REQUESTED

        /// <summary>
        /// Occurs when [association request received].
        /// </summary>
        public event AssociationRequestHandler AssociationRequestReceived;

        /// <summary>
        /// Raises the association request received.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="asc">The asc.</param>
        public void RaiseAssociationRequestReceived(Request req, Association asc)
        {
            if (AssociationRequestReceived != null)
            {
                AssociationRequestReceived(req, asc);
            }
        }

        //ASSOCIATION ACCEPTED

        /// <summary>
        /// Occurs when [association acceptance received].
        /// </summary>
        public event AssociationAcceptedHandler AssociationAcceptanceReceived;

        /// <summary>
        /// Raises the association acceptance received.
        /// </summary>
        /// <param name="acc">The acc.</param>
        /// <param name="asc">The asc.</param>
        public void RaiseAssociationAcceptanceReceived(Accept acc, Association asc)
        {
            if (AssociationAcceptanceReceived != null)
            {
                AssociationAcceptanceReceived(acc, asc);
            }
        }

        //ASSOCIATION REJECTED

        /// <summary>
        /// Occurs when [association rejection received].
        /// </summary>
        public event AssociationRejectedHandler AssociationRejectionReceived;

        /// <summary>
        /// Raises the association rejection received.
        /// </summary>
        /// <param name="rej">The rej.</param>
        /// <param name="asc">The asc.</param>
        public void RaiseAssociationRejectionReceived(Reject rej, Association asc)
        {
            if (AssociationRejectionReceived != null)
            {
                AssociationRejectionReceived(rej, asc);
            }
        }

        //RELEASE REQUEST

        /// <summary>
        /// Occurs when [release request received].
        /// </summary>
        public event ReleaseRequestHandler ReleaseRequestReceived;

        /// <summary>
        /// Raises the release request received.
        /// </summary>
        /// <param name="relReq">The relative req.</param>
        /// <param name="asc">The asc.</param>
        public void RaiseReleaseRequestReceived(ReleaseRequest relReq, Association asc)
        {
            if (ReleaseRequestReceived != null)
            {
                ReleaseRequestReceived(relReq, asc);
            }
        }

        //RELEASE RESPONSE

        /// <summary>
        /// Occurs when [release response received].
        /// </summary>
        public event ReleaseResponseHandler ReleaseResponseReceived;

        /// <summary>
        /// Raises the release response received.
        /// </summary>
        /// <param name="relRs">The relative rs.</param>
        /// <param name="asc">The asc.</param>
        public void RaiseReleaseResponseReceived(ReleaseResponse relRs, Association asc)
        {
            if (ReleaseResponseReceived != null)
            {
                ReleaseResponseReceived(relRs, asc);
            }
        }

        //ABORT REQUEST

        /// <summary>
        /// Occurs when [abort request received].
        /// </summary>
        public event AbortRequestHandler AbortRequestReceived;

        /// <summary>
        /// Raises the abort request received.
        /// </summary>
        /// <param name="abort">The abort.</param>
        /// <param name="asc">The asc.</param>
        public void RaiseAbortRequestReceived(Abort abort, Association asc)
        {
            if (AbortRequestReceived != null)
            {
                AbortRequestReceived(abort, asc);
            }
        }

        #endregion
    }
}