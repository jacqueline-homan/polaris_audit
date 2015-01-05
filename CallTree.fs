﻿namespace Polaris

module Types =


    type Caller =
        | Victim
        | Survivor
        | Advocate

    type Ngo =
        | VictimSafehouse
        | HomelessShelter
        | PovertyRelief
        | MedicalDentalCare
        | SurvivorAid

    type Followup =
        | NotFollowedUp // No one followed up with caller
        | FollowedUp of Help //Follow-upper obtains help or referral for caller
         

    and Help = //Whether the NGO Polaris referred callerr to helped caller
        | Helped //Caller gets the help they needed
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
        | SurvivorAssistance //aid for destitute survivor
      

    type Call = Call of Caller * CallerRequest * CallOutcome 

  

