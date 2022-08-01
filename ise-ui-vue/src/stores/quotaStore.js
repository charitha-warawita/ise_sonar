import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 
export const useQuotaStore = defineStore('quota', {
    state: () => ({
        selecteDiv:false,
        conditionlist:[],
        selectedConditionName:"",
        selectedConditionText:"",
        selectedConditioncategoryName:"",
        Selectedvariables:"",
        selecttedconditions:[],
        currAgeRange: '',
        quotaMinAge: 0,
        quotaMaxAge:0,
        error: null,
        conditiongrid:false,
        iseUrl: import.meta.env.VITE_ISE_API_URL,
        iseCountryPath: import.meta.env.VITE_ISE_API_COUNTRIES,

        currentQuota: {
            name: '',
            serveyQuotaTypeSelected:'',
            adjustmentType:'',
            fieldTargetPercentage: 0,
            fieldTargetNominal:0,
            status: 0,
            completes: 0,
            prescreens: 0,
            quotaPercentage: 0,
            quotaNominal: 0,
            conditions: ["None", "Age", "Gender", "Country"],
            ServeyQuotaType:["Client","Control","Supplier"],
            quotaMinAge:0,
            quotaMaxAge:0,
            currAgeRange: '',
        }
        


    }),
    getters: {
        
    },
    actions: {
        LoadDefaultCurrentQuota() {
            this.currentQuota = {
                name: '',
                serveyQuotaTypeSelected:'',
                adjustmentType:'',
                fieldTargetPercentage: 0,
                fieldTargetNominal: 0,
                status: 0,
                completes: 0,
                prescreens: 0,
                quotaPercentage: 0,
                quotaNominal: 0,
                conditions: ["None", "Age", "Gender", "Country"],
                quotaMinAge:0,
                quotaMaxAge:0,
                ServeyQuotaType:["Client","Control","Supplier"],
            };
        },
           serveyQuotaTyp(event){
            this.currentQuota.serveyQuotaTypeSelected = event.target.value;    
        },
        addCondition(){
            this.selectedConditionName = "";
            this.selectedConditionText ="";
            this.selectedConditioncategoryName ="";
            this.Selectedvariables = "";
            this.selecteDiv =false;
            this.conditiongrid = true;
            this.conditionlist =[];
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
                            console.log(this.selecttedconditions)
                        }
                    }
            }
            
        },
        async SaveQuotaToProject( selecttedQuestionid, selectedConditioncategoryName, selectedConditionName, selectedConditionText, id, name) {
                console.log(selecttedQuestionid, selectedConditioncategoryName, selectedConditionName, selectedConditionText, id, name)
        },
        SaveQuota(taId, quotaid ,selecttedQuestionid, selectedConditioncategoryName, selectedConditionName, selectedConditionText, id, name){
            var project = useProjectStore().project;
            for (var i = 0; i < project.projectTargetAudiences.length; i++)
            {
                var quota = {};
                if(project.projectTargetAudiences[i].id === taId)
                {
                    quota = {
                        "id": quotaid,
                        "name": this.currentQuota.name,
                        "fieldTargetNominal": this.currentQuota.fieldTargetNominal,
                        "fieldTargetPercentage": this.currentQuota.fieldTargetPercentage,
                        "status": this.currentQuota.status,
                        "completes": this.currentQuota.completes,
                        "prescreence":this.currentQuota.prescreens,
                        "serveyQuotaTypeSelected": this.currentQuota.serveyQuotaTypeSelected,
                        "adjustmentType": this.currentQuota.adjustmentType,
                        "quotaPercentage": this.currentQuota.quotaPercentage,
                        "quotaNominal": this.currentQuota.quotaNominal,
                        "order": quotaid,
                        "logicalDecision": "OR",
                        "NumberOfRequiredConditions": 0,
                        "IsActive": true,
                        "conditions":[]
                        };
                }
                if(itemtype === 'age')
                {    
                    var condition = {
                        "id": 42,
                        "name": "Age",
                        "text": "Enter age range for the project",
                        "categoryName": "Household",
                        "variables": [
                            {
                                "id": 1,
                                "name": this.currentQuota.quotaMinAge
                            },
                            {
                                "id": 1,
                                "name": this.currentQuota.quotaMaxAge
                            }
                        ]
                    }
                    quota.conditions.push(condition);
                }
                if(itemtype === 'country')
                {
                    quota.conditions = [];
                    var condition = {
                        "id": 1,
                        "name": "Country",
                        "text": "Enter the countries",
                        "categoryName": "Household",
                        "variables": [
                            {
                                "id": 1,
                                "name": "UK"
                            }
                        ]
                    }
                    quota.conditions.push(condition);
                }
                if(itemtype === 'gender')
                {
                    quota.conditions = [];
                    var condition = {
                        "id": 43,
                        "name": "Gender",
                        "text": "Enter the genders of panelists",
                        "categoryName": "Household",
                        "variables": [
                            {
                                "id": 1,
                                "name": "Male"
                            },
                            {
                                "id": 2,
                                "name": "Female"
                            }
                        ]
                    }
                    quota.conditions.push(condition);
                } 

                project.projectTargetAudiences[i].quotas.push(quota);
                project.projectTargetAudiences[i].quota.push(quota);
                this.LoadDefaultCurrentQuota();
                break;
            }
            
        
        }

    }
})