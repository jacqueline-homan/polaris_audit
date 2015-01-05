namespace Polaris

module TerminalBuilder =
    open System
    open System.IO
    open Polaris.Types

//    let rec menu(header:string)(options:string[]) =
//        printfn "%s" header
//        let digits = [1..options.Length]
//        Seq.iter((function (i,o) -> printfn "%d %s" i o)
                

    let rec caller():Caller =
        printfn "Are you reporting as a victim, survivor, or advocate?"
        printfn "Enter 1 for Victim"
        printfn "Enter 2 for Survivor"
        printfn "Enter 3 for Advocate"
        let response = Console.ReadLine()
        match response.Trim() with
            | "1" -> Victim
            | "2" -> Survivor
            | "3" -> Advocate
            | _ -> printfn "Invalid Response"
                   caller()

    let rec ngo():Ngo =
        printfn "What kind of NGO were you referred to?"
        printfn "Enter 1 for Sex Trafficking Victim Safehouse"
        printfn "Enter 2 for Emergency Homeless Shelter"
        printfn "Enter 3 for Poverty Relief"
        printfn "Enter 4 for Survivor Medical and Dental Care"
        printfn "Enter 5 for General Survivor Aid" 
        let response = Console.ReadLine()
        match response.Trim() with
            | "1" -> VictimSafehouse 
            | "2" -> HomelessShelter
            | "3" -> PovertyRelief
            | "4" -> MedicalDentalCare
            | "5" -> SurvivorAid
            | _ ->
                printfn "Invalid Option"
                ngo()
    
    let rec callerRequest(): CallerRequest =
        printfn "Please enter what help you requested"
        printfn "Enter 1 for Victim Services"
        printfn "Enter 2 for Survivor Support and Assistance"
        printfn "Enter 3 for reporting trafficking in progress"
        let answer = Console.ReadLine()
        match answer with
            | "1" -> VictimServices 
            | "2" -> SurvivorAssistance
            | "3" -> PoliceDispatch
            | _ -> printfn "Invalid option"
                   callerRequest()                                  
    
    
    let rec followup()=
        printfn "Did anyone follow up with you?"
        printfn "1 for Yes"
        printfn "2 for No"
        let reply = Console.ReadLine()
        match reply.Trim() with
            | "1" -> FollowedUp (helpbuilder())                     
                    
            | "2" -> NotFollowedUp  
            | _ -> printfn "Invalid option"
                   followup()

    and helpbuilder():Help = 
        printfn "What kind of help did you get?"
        printfn "Enter 1 if You got the help you needed"
        printfn "Enter 2 if Not Helped and all options were exhausted"
        printfn "Enter 3 if Denied Help"
        printfn "Enter 4 if You were offered the WRONG help"
        printfn "Enter 5 if You were Not Helped But Referred to another NGO"
        let response = Console.ReadLine()
        match response.Trim() with
            | "1" -> Helped
            | "2" -> RanOutOfHelps
            | "3" -> NotHelped (followup())
            | "4" -> WrongHelp (followup())
            | "5" -> Referred (refbuilder())                
            | _ -> 
                   printfn "Invalid Option"
                   helpbuilder()

    and refbuilder():CallerRefToOtherNgo =
        let n = ngo()
        let f = followup()
        CallerRefToOtherNgo (f, n)            
                
    
    let rec police_disp():PoliceDisp =
        printfn "Did police rescue victim? (Enter Y for Yes and N for No:"
        let reply = Console.ReadLine()
        match reply.Trim() with
            | "Y" -> VictimRescued
            | "N" -> CopsNoHelp (followup())
            | _ -> printfn "Invalid Selection"
                   police_disp()

    let rec call_outcome():CallOutcome =
        printfn "What help did Polaris provide?"
        printfn "Enter 1 for Polaris provided victim/survivor aid to me"
        printfn "Enter 2 for Polaris operator dispatched 911"
        printfn "Enter 3 for Polaris referred me to another NGO"
        printfn "Enter 4 for Polaris did not help me"
        printfn "Enter 5 for call to hotline got disconnected"
        let reply = Console.ReadLine()
        match reply.Trim() with
            | "1" -> ProvideDirectHelpToVictimOrSurvivor  
            | "2" -> EmergencyResponse (police_disp())//need function that creates the ngo stuff
            | "3" -> CallerRef (refbuilder())
            | "4" -> FailedToHelpCaller (followup())
            | "5" -> DisconnectCall (followup())
            | _ -> printfn "Invalid selection"
                   call_outcome()

                   
