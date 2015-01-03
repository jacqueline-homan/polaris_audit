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
        printfn "Were you referred to another agency?"
        printfn "1 for Yes"
        printfn "2 for No"
        let reply = Console.ReadLine()
        match reply.Trim() with
            | "1" -> 
                     printfn "Did anyone follow up with you?"
                     printfn "1 for Yes"
                     printfn "2 for No"
                     let reply = Console.ReadLine()
                     match reply.Trim() with
                         | "1" -> Followup true  
                         | "2" -> Followup false
                         | _ ->
                                printfn "Invalid option"
                                followup()
                    
            | "2" -> Followup false
            | _ -> printfn "Invalid option"
                   followup()

    let rec helpbuilder():Help = 
        printfn "Did you get the help you needed?"
        printfn "1 for Yes"
        printfn "2 for No"
        let response = Console.ReadLine()
        match response.Trim() with
            | "1" -> Helped
            | "2" -> NotHelped (followup())
                
            | _ -> 
                   printfn "Invalid Option"
                   helpbuilder()

    let rec refbuilder():CallerRefToOtherNgo =
        printfn "What kind of angency were you referred to?"
        printfn "1 for Sex Trafficking Victim Safehouse"
        printfn "2 for Labor Trafficking Agency"
        printfn "3 for Survivor Assistance Agency"
        let response = Console.ReadLine()
        match response.Trim() with
            | "1" -> VictimSafehouse (helpbuilder())
            | "2" -> LaborTraffickingNgo (helpbuilder())
            | "3" -> SurvivorAssistanceNgo (helpbuilder())
            | _ ->
                printfn "Invalid Option"
                refbuilder()
    
    let rec police_disp():PoliceDisp =
        printfn "Did police rescue victim? (Enter Y for Yes and N for No:"
        let reply = Console.ReadLine()
        match reply with
            | "Y" -> VictimRescued
            | "N" -> CopsNoShow (refbuilder())
            | _ -> printfn "Invalid Selection"
                   police_disp()

    let rec polaris_action():PolarisAction =
        printfn "What help did Polaris provide?"
        printfn "1 for 911 coordination and dispatch"
        printfn "2 for Polaris referred me to another NGO"
        printfn "3 for Polaris operator disconnected call"
        printfn "4 for Polaris did not help me"
        printfn "5 for Polaris provided direct help to me/victim/survivor"
        let reply = Console.ReadLine()
        match reply with
            | "1" -> EmergencyResponse (police_disp())//need function to create a policedisp 
            | "2" -> CallerRef (refbuilder())//need function that creates the ngo stuff
            | "3" -> DisconnectCall
            | "4" -> FailedToHelpCaller
            | "5" -> ProvideDirectHelpToVictimOrSurvivor
            | _ -> printfn "Invalid selection"
                   polaris_action()

                   
