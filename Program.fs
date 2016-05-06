
open System
open System.IO
open System.Collections
open System.Data
open System.Data.SqlClient
open System.Data.Linq
open Microsoft.FSharp.Data
open Newtonsoft.Json
open Polaris.Core.Types
open Polaris.TerminalBuilder

let rec ngoinfo(Ngo(cat, name)) =
    match cat with
        | VictimSafehouse -> printfn "Victim Safehouse: %s" name
        | HomelessShelter -> printfn "Homeless Shelter: %s" name
        | PovertyRelief -> printfn "Poverty Relief: %s" name
        | MedicalDentalCare -> printfn "Medical and Dental Help: %s" name
        | SurvivorAid -> printfn "Survivor Assistance: %s" name

let rec help(h:Help) =
//    let rn = h.
    match h with
    | Helped(CallResult(true), un) -> printfn "Got all the help requested and needed"
    | PartiallyHelped (crngo, un) -> printfn "Only partly helped and still have unmet basic needs"
                                     // So, hey, you got partially, helped. Did you get a referral, as well?
                                     let nrn = CallerRequest.SurvivorAssistance(needs())
                                     callerreftonextngo(crngo, nrn)
    | RanOutOfHelps(CallResult(false), un) -> printfn "Exhausted all referrals and still not helped"
    | NotHelped (fu, un) -> printfn "Denied help (possible discrimination?)"
                            followupper(fu)
    | WrongHelp (fu, un) -> printfn "Offered help but not the help I needed"
                            followupper(fu)
    | Referred(crngo, un) -> printfn "Not helped but referred to another NGO"
                             callerreftonextngo (crngo, un)
                         

and callerreftonextngo(CallerRefToOtherNgo(fu, ng), nrn) =
    printfn "Caller referred to another NGO:" 
    ngoinfo(ng)
    followupper(fu)

and followupper(fu:Followup) =
    match fu with
    | NotFollowedUp(h) -> printfn "No one followed up"
                          help(h)                       
    | FollowedUp(h) -> printfn "A caseworker followed up"
                       help(h)
    //| CallerSelfFollow(h) -> printfn "Caller self-reporting/no caseworker followup"
                             //help(h)


let outcome_of_poldisp(pd) = 
    match pd with
    | VictimRescued -> printfn "Victim got rescued"
    | CopsNoHelp (fu) -> 
        printfn "Cops no show and victim not rescued"
        followupper(fu)


let call_out(co:CallOutcome) (rn: CallerRequest) = //
    match co with
    | ProvideDirectHelpToVictimOrSurvivor -> printfn "Polaris directly helped victim or survivor"
    | EmergencyResponse (pd)  ->  
        printfn "Polaris operator dispatched 911"
        outcome_of_poldisp(pd)
        
    | CallerRef (crngo, rn) -> 
        printfn "Polaris referred me to another NGO"
        callerreftonextngo(crngo, rn)
    | DisconnectCall (fu) -> 
        printfn "Call got disconnected"
        followupper(fu)
    | FailedToHelpCaller (fu) -> printfn "Was not helped"
                                 followupper(fu)

let fx(un:Set<UnmetNeeds>)=
    Seq.iter(fun x ->
        match x with
        | UnmetNeeds.Legal -> printfn "Legal"
        | UnmetNeeds.Dental -> printfn "Dental"
        | UnmetNeeds.Medical -> printfn "Medical"
        | UnmetNeeds.Vison -> printfn "Vison"
        | UnmetNeeds.Hearing -> printfn "Hearing"
        | UnmetNeeds.TraumaTherapy -> printfn "Trauma Therapy"
        | UnmetNeeds.IncomeSupport -> printfn "Income Support"
        | UnmetNeeds.PermanentHousing -> printfn "Pernament Housing"
        | UnmetNeeds.EducationHelp -> printfn "Education Help"
        | UnmetNeeds.SkillsTraining -> printfn "Skills Training"
        | UnmetNeeds.JobPlacement  -> printfn "Job Placement"
        | _ -> printfn" ")(un)

let caller_req(cr:CallerRequest) = //Done
    match cr with
    | PoliceDispatch -> printfn "911 dispatch to rescue victim"
    | VictimServices -> printfn "Victim Services"
    | SurvivorAssistance(un) -> printfn "Survivor Aid"
                                fx(un) 

let caller_info(c:Caller) = //Done
    match c with
    | Survivor -> printfn "Survivor"
    | Advocate -> printfn "Advocate"

let call_info(Call(ca, cr, co)) = //Done
    printfn "****************************************"
    printfn "*  Caller Record of Help Requested      *"
    printfn "****************************************"

    caller_info(ca)
    caller_req(cr)
    call_out(co)
  

[<EntryPoint>]
let main argv = 
    let c = (caller())
    let cr = (callerRequest())
    let un = cr.GetUnmetNeeds()
    let rn = cr.GetUnmetNeeds()
    //let rn = (needs())
    
    let co = (call_outcome(cr))
    let ca = Call(c, cr,co)
//    call_info(ca)

    
    let js = JsonConvert.SerializeObject(ca)

    //automatically generates a json file
    use w = new StreamWriter("test.json", false)
    w.Write(js) 
 
    let ca2 = JsonConvert.DeserializeObject<Call>(js)
    call_info(ca2)
     


//    printfn "%A" argv
    0 // return an integer exit code

