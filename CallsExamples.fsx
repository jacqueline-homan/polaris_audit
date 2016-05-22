// This script creates a couple of fake calls,
// purely in code, to help us test out whether
// the code works the way we want to, without
// having to waste time running a Console app.

#r @"/home/jacque/Projects/F-sharp/polaris_audit/Polaris.core/bin/Debug/Polaris.Core.dll"
open Polaris.Core
open Polaris.Core.Types

// this function will create a 'simple call':
// one referral only.
let ``simple call`` () =

    let caller = Survivor

    let needs = 
        set [ 
            UnmetNeeds.Legal
            UnmetNeeds.Medical
            UnmetNeeds.Dental
            UnmetNeeds.IncomeSupport 
            ]

    let request = SurvivorAssistance needs

    let referredNgo = Ngo(PovertyRelief, "Next NGO")

    let result = CallResult(true)

    let followUp = NotFollowedUp(Helped(result,request))

    let referral = CallerRefToOtherNgo(followUp, referredNgo)

    let outcome = CallOutcome.CallerRef(referral,request)

    // return the fully constructed 'simple call'
    Call(caller, request, outcome)

// this call has 3 levels of depth in
// the referral chain.
let ``3 levels call`` () =

    let caller = Survivor
    let needs = set [ UnmetNeeds.Dental; UnmetNeeds.IncomeSupport ]
    let request = SurvivorAssistance needs

    let firstNgo = Ngo(PovertyRelief, "NGO #1")
    let secondNgo = Ngo(MedicalDentalCare, "NGO #2")
    let thirdNgo = Ngo(PovertyRelief, "NGO #3")

    let result = CallResult(true)

//    let followUp = NotFollowedUp(Helped(result,request))
            
    let deepChain =
        CallerRefToOtherNgo(
            FollowedUp(
                Referred(
                    CallerRefToOtherNgo(
                        NotFollowedUp(
                            Referred(                
                                CallerRefToOtherNgo(
                                    FollowedUp(
                                        Helped(result,request)),
                                    thirdNgo
                                    ),
                                request)
                            ), 
                        secondNgo),
                    request)), 
            firstNgo)
               
    let outcome = CallOutcome.CallerRef(deepChain,request)

    Call(caller, request, outcome)
