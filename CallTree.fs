namespace Polaris

open System.Collections

module Types =  

   
    type SpeakerFees =
        | NotPaid
        | PaidAsAgreed
        | PaidLessThanAgreed


    type NgoJobs =
        | NotHired
        | Speaker of SpeakerFees
        | Counselor
        | Administrator
        | ProgramDirector
        | IT
        | Intern
        | JobPlacementAdvocate
        | SkillsInstructor
        | Intake
        | Clerical
    
    type Reporter = 
        | SurvivorJobseeker
        | SurvivorContractor
        | SurvivorOwnedFirm
        | HelpseekerAndJobseeker //caller at one time needed help but didn't 
                                // get helped, and at a different time, applied 
                                // for a job with an NGO and didn't get it.

    type RequestedNeeds = 
        | Legal 
        | Dental
        | Medical
        | Vison
        | Hearing
        | TraumaTherapy
        | IncomeSupport
        | PermanentHousing
        | EducationHelp
        | SkillsTraining
        | JobPlacement


    type Caller =
        | Victim
        | Survivor
        | Advocate

    
    type NgoType =
        | VictimSafehouse
        | HomelessShelter
        | PovertyRelief
        | MedicalDentalCare
        | SurvivorAid

    type Ngo = Ngo of NgoType * string
      
    type Followup =
        | NotFollowedUp // No one followed up with caller
        | FollowedUp of Help //Follow-upper obtains help or referral for caller
        | CallerSelfFollow of Help // Victim/survivor left to navigate on own
         

    and Help = //Whether the NGO Polaris referred callerr to helped caller
        | Helped //Caller gets the help they needed
        | HelpFail //Caller got revictimized by the "rescuers"
        | RanOutOfHelps //Follow-upper or caller has exhausted all possible options
        | NotHelped of Followup // Not helped (possible discrimination? Lack of resources?)
        | WrongHelp of Followup //Offered help but not the kind of help that was needed
        | Referred of CallerRefToOtherNgo //Not helped but referred to another NGO
      

     and Revictimization =
        | Rape of Help // Caller got sexually harrassed or assaulted at safehouse
        | SlaveLabor of Help //Caller was forced to work without pay making goods for NGO to sell
        | VictimBlame of Help //Caller was blamed for being sex trafficked by Christian counselors
        | KickedOut of Help //Caller was kicked out of safehous with nowhere to go


    and CallerRefToOtherNgo = CallerRefToOtherNgo of Followup * Ngo       


  
    type PoliceDisp =
        | VictimRescued
        | CopsNoHelp of Followup
    

    type CallOutcome =
        | ProvideDirectHelpToVictimOrSurvivor
        | EmergencyResponse of PoliceDisp
        | CallerRef of CallerRefToOtherNgo
        | DisconnectCall of Followup
        | FailedToHelpCaller of Followup


    type CallerRequest =
        | PoliceDispatch //911 coordination for trafficking in progress
        | VictimServices //emergency shelter in victim safehouse
        | SurvivorAssistance of  Set<RequestedNeeds> //aid for destitute survivor
      

    type Call = Call of Caller * CallerRequest * CallOutcome 

     

