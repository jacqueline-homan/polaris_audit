namespace Polaris.Core

open System.Collections

module Types =

    type RequestedNeeds =
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
      
    type Followup =
        | NotFollowedUp of Help// No one followed up with caller
        | FollowedUp of Help //Follow-upper obtains help or referral for caller
        //| CallerSelfFollow of Help // Victim/survivor left to navigate on own
         

    and Help = //Whether the NGO Polaris referred callerr to helped caller
        | Helped //Caller gets the help they needed
        | PartiallyHelped of CallerRefToOtherNgo //Survivor only gets some of their urgent unmet needs met but not all
        | RanOutOfHelps //Follow-upper or caller has exhausted all possible options
        | NotHelped of Followup // Not helped (possible discrimination? Lack of resources?)
        | WrongHelp of Followup //Offered help but not the kind of help that was needed
        | Referred of CallerRefToOtherNgo //Not helped but referred to another NGO


    and CallerRefToOtherNgo = CallerRefToOtherNgo of Followup * Ngo


    type PoliceDisp =
        | VictimRescued
        | CopsNoHelp of Followup


    type CallOutcome =
        | ProvideDirectHelpToVictimOrSurvivor
        | EmergencyResponse of PoliceDisp
        | CallerRef of CallerRefToOtherNgo
        | DisconnectCall of Followup
        | FailedToHelpCaller of Followup


    type CallerRequest =
        | PoliceDispatch //911 coordination for trafficking in progress
        | VictimServices //emergency shelter in victim safehouse
        | SurvivorAssistance of  Set<RequestedNeeds> //aid for destitute survivor


    type Call = Call of Caller * CallerRequest * CallOutcome


    type ReportSubmission =
        {
            ngo:            Ngo
            callerType:     Caller
            ngoType:        NgoType
            referrer:       Ngo
            gotFollowup:    bool
            
        }

