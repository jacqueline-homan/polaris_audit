// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System
open System.IO
open Polaris.Types
open Polaris.TerminalBuilder

let rec ngoinfo(Ngo(cat, name)) =
    match cat with
        | VictimSafehouse -> printfn "Victim Safehouse: **%s**" name
        | HomelessShelter -> printfn "Homeless Shelter: **%s**" name
        | PovertyRelief -> printfn "Poverty Relief: **%s**" name
        | MedicalDentalCare -> printfn "Medical and Dental Help: **%s**" name
        | SurvivorAid -> printfn "Survivor Assistance: **%s**" name

let rec help(h:Help) =
    match h with
    | Helped -> printfn "Got all the help requested and needed"
    | RanOutOfHelps -> printfn "Exhausted all referrals and still not helped"
    | NotHelped (fu) -> printfn "Denied help (possible discrimination?)"
                        followupper(fu)
    | WrongHelp (fu) -> printfn "Offered help but not the help I needed"
                        followupper(fu)
    | Referred(crngo) -> printfn "Not helped but referred to another NGO"
                         callerreftonextngo(crngo)
                         

and callerreftonextngo(CallerRefToOtherNgo(fu, ng)) =
    printfn "Caller referred to another NGO:" 
    ngoinfo(ng)
    followupper(fu)

and followupper(fu:Followup) =
    match fu with
    | NotFollowedUp -> printfn "No one followed up"
    | FollowedUp(h) -> printfn "A caseworker followed up"
                       help(h)


let outcome_of_poldisp(pd) = 
    match pd with
    | VictimRescued -> printfn "Victim got rescued"
    | CopsNoHelp (fu) -> 
        printfn "Cops no show and victim not rescued"
        followupper(fu)


let call_out(co:CallOutcome) = //
    match co with
    | ProvideDirectHelpToVictimOrSurvivor -> printfn "Polaris directly helped victim or survivor"
    | EmergencyResponse (pd)  ->  
        printfn "Polaris operator dispatched 911"
        outcome_of_poldisp(pd)
        
    | CallerRef (crngo) -> 
        printfn "Polaris referred me to another NGO"
        callerreftonextngo(crngo)
    | DisconnectCall (fu) -> 
        printfn "Call got disconnected"
        followupper(fu)
    | FailedToHelpCaller (fu) -> printfn "Was not helped"

let caller_req(cr:CallerRequest) = //Done
    match cr with
    | PoliceDispatch -> printfn "911 dispatch to rescue victim"
    | VictimServices -> printfn "Victim Services"
    | SurvivorAssistance -> printfn "Survivor Aid"


let caller_info(c:Caller) = //Done
    match c with
    | Victim -> printfn "Victim"
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
    let co = (call_outcome())
    let ca = Call(c, cr,co)
    call_info(ca)


//    printfn "%A" argv
    0 // return an integer exit code

