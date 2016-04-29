namespace Polaris

module TerminalBuilder =
    open System
    open System.IO
    open Polaris.Core.Types
                

    let rec caller():Caller =
        printfn "Are you reporting as a victim, survivor, or advocate?"
//        printfn "Enter 1 for Victim"
        printfn "Enter 1 for Survivor"
        printfn "Enter 2 for Advocate"
        let response = Console.ReadLine()
        match response.Trim() with
//            | "1" -> Victim
            | "1" -> Survivor
            | "2" -> Advocate
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
        printfn "Enter the name of the NGO you were referred to:"
        let resp = Console.ReadLine()
        printfn "%s" resp
        match response.Trim() with
            | "1" -> Ngo (VictimSafehouse, resp)
            | "2" -> Ngo (HomelessShelter, resp)
            | "3" -> Ngo(PovertyRelief, resp)
            | "4" -> Ngo(MedicalDentalCare, resp)
            | "5" -> Ngo (SurvivorAid, resp)
            | _ ->
                printfn "Invalid Option"
                ngo()

    let needs():Set<RequestedNeeds> =
        let rec nds(s:Set<RequestedNeeds>):Set<RequestedNeeds> = 
            printfn "What unmet need(s) did you request help with?"
            printfn "Enter one or more of these:"
            printfn "Legal, Dental, Medical, Vison, Hearing"
            printfn "Trauma Therapy, Income Support, Permanent Housing"
            printfn "Educational Help, Skills Training, Job Placement"
            printfn "Enter 'Done' when finished"
            let response = Console.ReadLine()
            match response.Trim().ToLower() with
            | "done" -> s
            | _ ->
                let n =
                    match response.Trim().ToLower() with
                    | "legal" -> Some RequestedNeeds.Legal
                    | "dental" -> Some RequestedNeeds.Dental
                    | "medical" -> Some RequestedNeeds.Medical
                    | "vison" -> Some RequestedNeeds.Vison
                    | "hearing" -> Some RequestedNeeds.Hearing
                    | "trauma therapy" -> Some RequestedNeeds.TraumaTherapy
                    | "income support" -> Some RequestedNeeds.IncomeSupport
                    | "permanent housing" -> Some RequestedNeeds.PermanentHousing
                    | "educational help" -> Some RequestedNeeds.EducationHelp
                    | "skills training" -> Some RequestedNeeds.SkillsTraining
                    | "job placement" -> Some RequestedNeeds.JobPlacement
                    | _ -> printfn "Invalid entry"
                           None
                match n with
                | None -> nds(s)
                | Some(x) -> nds(s.Add(x))
                             
        
        nds(new Set<RequestedNeeds>([]))
         

    let rec callerRequest(): CallerRequest =
        printfn "Please enter what help you requested"
        printfn "Enter 1 for Victim Services"
        printfn "Enter 2 for Survivor Support and Assistance"
        printfn "Enter 3 for reporting trafficking in progress"
        let answer = Console.ReadLine()
        match answer with
            | "1" -> VictimServices 
            | "2" -> SurvivorAssistance (needs())
            | "3" -> PoliceDispatch
            | _ -> printfn "Invalid option"
                   callerRequest()                                  
    
   

    let rec followup()=
        printfn "Did anyone follow up with you?"
        printfn "1 for Yes"
        printfn "2 for No"
        printfn "3 for self-reporting/no caseworker followup"
        let reply = Console.ReadLine()
        match reply.Trim() with
            | "1" -> FollowedUp (helpbuilder())           
            | "2" -> NotFollowedUp (helpbuilder())
            //| "3" -> CallerSelfFollow (helpbuilder())
            | _ -> printfn "Invalid option"
                   followup()

    and helpbuilder():Help = 
        printfn "What kind of help did you get?"
        printfn "Enter 1 if You got all the help you needed"
        printfn "Enter 2 if Not Helped and all options were exhausted"
        printfn "Enter 3 if Denied Help"
        printfn "Enter 4 if You were offered the WRONG help"
        printfn "Enter 5 if You were helped with only SOME of your unmet needs"
        printfn "Enter 6 if You were Not Helped But Referred to another NGO"
        let response = Console.ReadLine()
        match response.Trim() with
            | "1" -> Helped
            | "2" -> RanOutOfHelps
            | "3" -> NotHelped (followup())
            | "4" -> WrongHelp (followup())
            | "5" -> PartiallyHelped (refbuilder())
            | "6" -> Referred (refbuilder())                
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

                   
