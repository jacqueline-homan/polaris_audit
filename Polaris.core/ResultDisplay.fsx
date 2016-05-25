#load "CallTree.fs"
open Polaris.Core
open Polaris.Core.Types

let caller = Survivor

// why do I need qualified access?
let needs = 
    set [ 
        UnmetNeeds.Dental
        UnmetNeeds.IncomeSupport 
        ]

let request = SurvivorAssistance needs

let referredNgo = Ngo(PovertyRelief, "Next NGO")

// what does true and false represent?
let result = CallResult(true)

// what does this mean? Who is following up?
let followUp = NotFollowedUp(Helped(result,request))

let referral = CallerRefToOtherNgo(followUp, referredNgo)

let outcome = CallOutcome.CallerRef(referral,request)

let call = Call(caller, request, outcome)

// printout summary

// 1. Survivor or Advocate

let callerSummary (call:Call) =
    match call with
    | Call(caller,_,_) ->
        match caller with
        | Survivor -> "You are a survivor"
        | Advocate -> "You are an advocate"

         
