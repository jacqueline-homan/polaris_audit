namespace Polaris

module TerminalBuilder =
    open System
    open System.IO
    open FSharp.Collections
    open Polaris.Core.Types


    let rec caller():Caller =
        printfn "Are you reporting as a victim, survivor, or advocate?"
        printfn "Enter 1 for Survivor"
        printfn "Enter 2 for Advocate"
        let response = Console.ReadLine()
        match response.Trim() with
            | "1" -> Survivor
            | "2" -> Advocate
            | _   -> printfn "Invalid Response"
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
            | _   -> printfn "Invalid Option"
                     ngo()

    let needs():Set<UnmetNeeds> =
        let rec nds(s:Set<UnmetNeeds>):Set<UnmetNeeds> = 
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
                    | "legal"             -> Some UnmetNeeds.Legal
                    | "dental"            -> Some UnmetNeeds.Dental
                    | "medical"           -> Some UnmetNeeds.Medical
                    | "vison"             -> Some UnmetNeeds.Vison
                    | "hearing"           -> Some UnmetNeeds.Hearing
                    | "trauma therapy"    -> Some UnmetNeeds.TraumaTherapy
                    | "income support"    -> Some UnmetNeeds.IncomeSupport
                    | "permanent housing" -> Some UnmetNeeds.PermanentHousing
                    | "educational help"  -> Some UnmetNeeds.EducationHelp
                    | "skills training"   -> Some UnmetNeeds.SkillsTraining
                    | "job placement"     -> Some UnmetNeeds.JobPlacement
                    | _                   -> printfn "Invalid entry"
                                             None
                match n with
                | None -> nds(s)
                | Some(x) -> nds(s.Add(x))
        
        nds(new Set<UnmetNeeds>([]))

    // TODO: untested code, written by whim - jeroldhaas
    let metneeds (unmetNeeds: Set<UnmetNeeds>) :Set<UnmetNeeds> =
        let rec umnds (s :Set<UnmetNeeds>) :Set<UnmetNeeds> =
            printfn "What needs did you get help with?"
            printfn "Enter one or more of these:"
            printfn "Legal, Dental, Medical, Vision, Hearing"
            printfn "Trauma Therapy, Income Support, Permanent Housing"
            printfn "Educational Help, Skills Training, Job Placement"
            printfn "Enter 'Done' when finished"
            let response = Console.ReadLine()
            match response.Trim().ToLower() with
            | "done" -> s
            | _ ->
                let n =
                    match response.Trim().ToLower() with
                    | "legal"             -> Some UnmetNeeds.Legal
                    | "dental"            -> Some UnmetNeeds.Dental
                    | "medical"           -> Some UnmetNeeds.Medical
                    | "vison"             -> Some UnmetNeeds.Vison
                    | "hearing"           -> Some UnmetNeeds.Hearing
                    | "trauma therapy"    -> Some UnmetNeeds.TraumaTherapy
                    | "income support"    -> Some UnmetNeeds.IncomeSupport
                    | "permanent housing" -> Some UnmetNeeds.PermanentHousing
                    | "educational help"  -> Some UnmetNeeds.EducationHelp
                    | "skills training"   -> Some UnmetNeeds.SkillsTraining
                    | "job placement"     -> Some UnmetNeeds.JobPlacement
                    | _                   -> printfn "Invalid entry"
                                             None
                match n with
                | None    -> umnds(s)
                | Some(x) -> if s.Contains(x) then umnds(s.Remove(x))
                             else umnds(s)
        umnds(unmetNeeds)

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
    
        let rec followup(cr: CallerRequest) =
        printfn "Did anyone follow up with you?"
        printfn "1 for Yes"
        printfn "2 for No"      
        let reply = Console.ReadLine()
        match reply.Trim() with
            | "1" -> FollowedUp (helpbuilder(cr))           
            | "2" -> NotFollowedUp (helpbuilder(cr))
            //| "3" -> CallerSelfFollow (helpbuilder())
            | _ -> printfn "Invalid option"
                   followup(cr)

    and helpbuilder (cr: CallerRequest) :Help = 
        printfn "What kind of help did you get?"
        printfn "Enter 1 if You got all the help you needed"
        printfn "Enter 2 if Not Helped and all options were exhausted"
        printfn "Enter 3 if Denied Help"
        printfn "Enter 4 if You were offered the WRONG help"
        printfn "Enter 5 if You were helped with only SOME of your unmet needs"
        printfn "Enter 6 if You were Not Helped But Referred to another NGO"
        let response = Console.ReadLine()
        match response.Trim() with
            | "1" -> Helped(CallResult(true), cr)
            | "2" -> RanOutOfHelps(CallResult(true), cr)
            | "3" -> NotHelped (followup(cr), cr)
            | "4" -> WrongHelp (followup(cr), cr)
            // HACK: did some shimming in bits here. Perhaps there's another | better way? - jeroldhaas
            | "5" -> let mn = metneeds(cr.GetUnmetNeeds())
                     let newcr = SurvivorAssistance(mn)
                     PartiallyHelped (refbuilder(newcr), newcr)
            | "6" -> Referred (refbuilder(cr), cr)
            | _ ->
                  printfn "Invalid Option"
                  helpbuilder(cr)

    and refbuilder (cr: CallerRequest): CallerRefToOtherNgo =
        let n = ngo()
        let f = followup(cr)
        CallerRefToOtherNgo (f, n)

    let rec police_disp(cr: CallerRequest):PoliceDisp =
        printfn "Did police rescue victim? (Enter Y for Yes and N for No:"
        let reply = Console.ReadLine()
        match reply.Trim() with
            | "Y" -> VictimRescued
            | "N" -> CopsNoHelp (followup(cr))
            | _ -> printfn "Invalid Selection"
                   police_disp(cr)

    let rec call_outcome (cr: CallerRequest): CallOutcome =
        printfn "What help did Polaris provide?"
        printfn "Enter 1 for Polaris provided victim/survivor aid to me"
        printfn "Enter 2 for Polaris operator dispatched 911"
        printfn "Enter 3 for Polaris referred me to another NGO"
        printfn "Enter 4 for Polaris did not help me"
        printfn "Enter 5 for call to hotline got disconnected"
        let reply = Console.ReadLine()
        match reply.Trim() with
            | "1" -> ProvideDirectHelpToVictimOrSurvivor  
            | "2" -> EmergencyResponse (police_disp(cr))//need function that creates the ngo stuff
            | "3" -> CallerRef (refbuilder(cr), cr)
            | "4" -> FailedToHelpCaller (followup(cr))
            | "5" -> DisconnectCall (followup(cr))
            | _ -> printfn "Invalid selection"
                   call_outcome(cr)
