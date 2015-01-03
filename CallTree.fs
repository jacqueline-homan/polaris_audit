namespace Polaris

module Types =


    type Caller =
        | Victim
        | Survivor
        | Advocate

    type Followup = Followup of bool 

    type Help = //Whether the NGO Polaris referred user to helped or not
        | Helped
        | NotHelped of Followup

    type CallerRefToOtherNgo =
        | VictimSafehouse of Help
        | LaborTraffickingNgo of Help
        | SurvivorAssistanceNgo of Help      

  
    type PoliceDisp =
        | VictimRescued
        | CopsNoShow of CallerRefToOtherNgo
    

    type PolarisAction =
        | EmergencyResponse of PoliceDisp
        | CallerRef of CallerRefToOtherNgo
        | DisconnectCall
        | FailedToHelpCaller
        | ProvideDirectHelpToVictimOrSurvivor

    type CallerRequest =
        | PoliceDispatch //911 coordination for trafficking in progress
        | VictimServices //emergency shelter in victim safehouse
        | SurvivorAssistance //aid for destitute survivor
      

    type Polaris = Polaris of Caller * CallerRequest * PolarisAction 

  

