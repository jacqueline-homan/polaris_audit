﻿namespace Polaris

module Types =


    type Caller =
        | Victim
        | Survivor
        | Advocate

    type Followup = Followup of bool 

    type Help =
        | Helped
        | NotHelped of (CallerRefToOtherNgo * Followup) option

    and CallerRefToOtherNgo =
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

    type Polaris = Polaris of Caller * PolarisAction

  

