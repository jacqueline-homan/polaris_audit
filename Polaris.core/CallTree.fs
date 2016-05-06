namespace Polaris.Core
open System.Collections

module Types =

    type UnmetNeeds =
        | Legal            = 1
        | Dental           = 2
        | Medical          = 3
        | Vison            = 4
        | Hearing          = 5
        | TraumaTherapy    = 6
        | IncomeSupport    = 7
        | PermanentHousing = 8
        | EducationHelp    = 9
        | SkillsTraining   = 10
        | JobPlacement     = 11


    type Caller =
        | Survivor
        | Advocate


    type NgoType =
        | VictimSafehouse
        | HomelessShelter
        | PovertyRelief
        | MedicalDentalCare
        | SurvivorAid

    type Ngo = Ngo of NgoType * string


    type CallerRequest =
        | PoliceDispatch //911 coordination for trafficking in progress
        | VictimServices //emergency shelter in victim safehouse
        | SurvivorAssistance of Set<UnmetNeeds> //aid for destitute survivor
        member cr.GetUnmetNeeds() =
                match cr with
                | SurvivorAssistance(un) -> un
                | _                     -> Set.empty

    type Followup =
        | NotFollowedUp of Help// No one followed up with caller
        | FollowedUp of Help //Follow-upper obtains help or referral for caller
        //| CallerSelfFollow of Help // Victim/survivor left to navigate on own
         
    and CallResult = CallResult of bool // TODO: underlying type for this is stubbed
    
    and Help = //Whether the NGO Polaris referred callerr to helped caller
        | Helped of CallResult * CallerRequest //Caller gets the help they needed
        | PartiallyHelped of CallerRefToOtherNgo * CallerRequest //Survivor only gets some of their urgent unmet needs met but not all
        | RanOutOfHelps of CallResult * CallerRequest //Follow-upper or caller has exhausted all possible options
        | NotHelped of Followup * CallerRequest // Not helped (possible discrimination? Lack of resources?)
        | WrongHelp of Followup * CallerRequest //Offered help but not the kind of help that was needed
        | Referred of CallerRefToOtherNgo * CallerRequest //Not helped but referred to another NGO

    and CallerRefToOtherNgo = CallerRefToOtherNgo of Followup * Ngo


    type PoliceDisp =
        | VictimRescued
        | CopsNoHelp of Followup


    type CallOutcome =
        | ProvideDirectHelpToVictimOrSurvivor
        | EmergencyResponse of PoliceDisp
        | CallerRef of CallerRefToOtherNgo * CallerRequest
        | DisconnectCall of Followup
        | FailedToHelpCaller of Followup





    type Call = Call of Caller * CallerRequest * CallOutcome


    type ReportSubmission =
        {
            Ngo:            Ngo
            CallerType:     Caller
            NgoType:        NgoType
            Referrer:       Ngo
            GotFollowup:    bool
        }

