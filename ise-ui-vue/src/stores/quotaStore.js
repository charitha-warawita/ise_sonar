import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 
export const useQuotaStore = defineStore('quota', {
    state: () => ({
        currentQuota: SetDefaultCurrentQuota(),



        selecteDiv:false,
        conditionlist:[],
        projectid: useProjectStore.globalTaId,
        selectedConditionId:'',
        selectedConditionName:'',
        selectedConditionText:'',
        selectedConditioncategoryName:'',
        Selectedvariables:'',
        selecttedconditions:[],
        selecttedAnswer:[],
        currAgeRange: '',
        quotaMinAge: 0,
        quotaMaxAge:0,
        error: null,
        conditiongrid:false,
        iseUrl: import.meta.env.VITE_ISE_API_URL,
        iseCountryPath: import.meta.env.VITE_ISE_API_COUNTRIES,

        currentQuota: {
            name: '',
            selectedServeyQuotaType:'',
            selectedAdjustmentType:'',
            fieldTargetPercentage: 0,
            fieldTargetNominal:0,
            status: 0,
            completes: 0,
            prescreens: 0,
            quotaPercentage: 0,
            quotaNominal: 0,
            // conditions: [],
            conditionId:'',
            conditionName:'',
            conditiontext:'',
            conditionCategoryName:'',
            variableId:'',
            variableName:'',
            ServeyQuotaType:["Client","Control","Supplier"],
            adjustmentType:["Nominal","percentage"],
            quotaMinAge:0,
            quotaMaxAge:0,
            currAgeRange: '',
        }
        


    }),
    getters: {
        
    },
    actions: {
        SetDefaultCurrentQuota() {
            return {
                tempId: 0,
                quotaName: '',
                quotaType: [{'name': 'Started', Selected: false }, {'name': 'Completed', selected: false }],
                fieldTarget: 0,
                fieldTargetPercentage: 0,
                limit: 0,
                limitPercentage: 0,
                adjustmentType: [{'name': 'Nominal', selected: false }, { 'name': 'percentage', selected: false }],
                prescreens: 0,
                completes: 0,
                IsActive: true,
                conditions: []
            }
        },
        LoadDefaultCurrentQuota() {
            this.currentQuota = {
                name: '',
                selectedServeyQuotaType:'',
                selectedAdjustmentType:'',
                fieldTargetPercentage: 0,
                fieldTargetNominal: 0,
                status: 0,
                completes: 0,
                prescreens: 0,
                quotaPercentage: 0,
                quotaNominal: 0,
                conditionId:'',
                conditionName:'',
                conditiontext:'',
                conditionCategoryName:'',
                variableId:'',
                variableName:'',
                quotaMinAge:0,
                quotaMaxAge:0,
                ServeyQuotaType:["Client","Control","Supplier"],
                adjustmentType:["Nominal","percentage"],
            };
        },
           serveyQuotaTyp(event){
            this.currentQuota.selectedServeyQuotaType = event.target.value;    
        },
        adjustmentType(event){
            this.currentQuota.selectedAdjustmentType = event.target.value;    
        },
        addCondition(){
            this.conditionlist = [];
            this.selectedConditionName = "";
            this.selectedConditionText ="";
            this.selectedConditioncategoryName ="";
            this.Selectedvariables = "";
            this.selecteDiv =false;
            this.conditiongrid = true;
            var project = useProjectStore().project;
            for (var i = 0; i < project.projectTargetAudiences.length; i++)
            {
                for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
                    {
                        this.conditionlist.push(project.projectTargetAudiences[i].qualifications[j].question)

                    }
            }

        },
        selectQuotaCondition(event){
            console.log(event.target.value.toLowerCase());
            var  selectedConditionId = parseInt(event.target.value);
            
            var project = useProjectStore().project;
            for (var i = 0; i < project.projectTargetAudiences.length; i++)
            {
                this.projectId = project.id;
                for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
              
                    {
                        if( project.projectTargetAudiences[i].qualifications[j].question.id === selectedConditionId)
                        {
     
                            this.selecttedQuestionid = project.projectTargetAudiences[i].qualifications[j].id
                            this.selectedConditionName = project.projectTargetAudiences[i].qualifications[j].question.name
                            this.selectedConditionText = project.projectTargetAudiences[i].qualifications[j].question.text
                            this.selectedConditioncategoryName = project.projectTargetAudiences[i].qualifications[j].question.categoryName
                            this.Selectedvariables = project.projectTargetAudiences[i].qualifications[j].question.variables;
                            this.selecteDiv =true
                        }
                    }
            }
            
        },
        SaveQuotaConditions( selecttedQuestionid, selectedConditioncategoryName, selectedConditionName, selectedConditionText, id, name) {
            this.currentQuota.conditionId = selecttedQuestionid; 
            this.currentQuota.conditionName = selectedConditionName;
            this.currentQuota.conditiontext = selectedConditionText;
            this.currentQuota.conditionCategoryName = selectedConditioncategoryName;
            this.currentQuota.variableId = id;
            this.currentQuota.variableName = name;
                // this.currentQuota.conditions.push({"id":selecttedQuestionid, "categoryName":selectedConditioncategoryName, "name":selectedConditionName, "text":selectedConditionText, "qId":id, "qName":name})
        },
        SaveQuota(itemtype, taid, quotaid,){
           
            var project = useProjectStore().project;
            for (var i = 0; i < project.projectTargetAudiences.length; i++)
            {
                var quota = {};

                    quota = {
                        "id": quotaid,
                        "name": this.currentQuota.name,
                        "fieldTargetNominal": this.currentQuota.fieldTargetNominal,
                        "fieldTargetPercentage": this.currentQuota.fieldTargetPercentage,
                        "status": this.currentQuota.status,
                        "completes": this.currentQuota.completes,
                        "prescreence":this.currentQuota.prescreens,
                        "serveyQuotaTypeSelected": this.currentQuota.selectedServeyQuotaType,
                        "adjustmentType": this.currentQuota.selectedAdjustmentType,
                        "quotaPercentage": this.currentQuota.quotaPercentage,
                        "quotaNominal": this.currentQuota.quotaNominal,
                        "order": quotaid,
                        "logicalDecision": "OR",
                        "NumberOfRequiredConditions": 0,
                        "IsActive": true,
                        "conditions": {
                            "id":  this.currentQuota.conditionId, "name":  this.currentQuota.conditionName, "text":  this.currentQuota.conditiontext, "categoryName":  this.currentQuota.conditionCategoryName,
                            "variables": [{ "id":  this.currentQuota.variableId, "name":  this.currentQuota.variableName }]
                        } 
                        };
                project.projectTargetAudiences[i].quota.push(quota);
                this.LoadDefaultCurrentQuota();
                break;
            }
            
        
        }

    }
})