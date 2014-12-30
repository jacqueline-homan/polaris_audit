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

    let rec helpbuilder():Help = 
        printfn "Did you get the help you needed?"
        printfn "1 for Yes"
        printfn "2 for No"
        let response = Console.ReadLine()
        match response.Trim() with
            | "1" -> Helped
            | "2" -> 
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
                            | "1" -> NotHelped (Some (refbuilder(), Followup true)) 
                            | "2" ->NotHelped (Some (refbuilder() , Followup false)) 
                            | _ ->
                                printfn "Invalid option"
                                helpbuilder()
                    
                    | "2" -> NotHelped None
                    | _ -> printfn "Invalid option"
                           helpbuilder()
             | _ -> 
                    printfn "Invalid Option"
                    helpbuilder()

    and refbuilder():CallerRefToOtherNgo =
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

                   
