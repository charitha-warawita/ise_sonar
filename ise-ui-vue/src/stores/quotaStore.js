import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 
export const useQuotaStore = defineStore('quota', {
    state: () => ({
        currAgeRange: '',
        error: null,
        showQuotaCondition:'',
        quotaCountries:[],
        quotaGenders:[],
        showSubPopup: false,

        currentQuota: {
            name: '',
            fieldTarget: 0,
            status: 0,
            completes: 0,
            prescreence: 0,
            conditions: ["None", "Age", "Gender", "Country"],
            quotaMinAge:0,
            quotaMaxAge:0,
        }
        


    }),
    getters: {
        
    },
    actions: {
        LoadDefaultCurrentQuota() {
            this.currentQuota = {
                name: '',
                fieldTarget: 0,
                status: 0,
                completes: 0,
                prescreence: 0,
                conditions: ["None", "Age", "Gender", "Country"],
                showQuotaCondition:'',
                quotaMinAge:0,
                quotaMaxAge:0,
            };
            this.showSubPopup = false;
        },
      async  GetQuota(itemtype, taId, qid) {
            if(itemtype === 'age')
            {
                var project = useProjectStore().project;
                for (var i = 0; i < project.projectTargetAudiences.length; i++)
                {
                    var projectQuotas=project.projectTargetAudiences
                    if(project.projectTargetAudiences[i].id === taId)
                    {
                        for(var j = 0; j < project.projectTargetAudiences[i].quota.length; j++)
                        {
                            if(project.projectTargetAudiences[i].quota[j].id === qid)
                            {
                                var variable = project.projectTargetAudiences[i].quota[j].condition.variables[0];
                                this.currAgeRange = variable.name;
                                break;
                            }
                        }
                    }
                }
            }
            if(itemtype === "country")
            {
                if(this.quotaCountries.length === 0)
                {
                    var countriesList = [{"id":1,"name":"UK","selected":true},{"id":2,"name":"Sweden","selected":false},{"id":3,"name":"Germany","selected":false},{"id":4,"name":"Denmark","selected":false},{"id":5,"name":"Finland","selected":false},{"id":6,"name":"Norway","selected":false},{"id":9,"name":"Spain","selected":false},{"id":10,"name":"Italy","selected":false},{"id":11,"name":"Hungary","selected":false},{"id":12,"name":"Greece","selected":false},{"id":22,"name":"USA","selected":false}];
                    this.quotaCountries = countriesList;
                }
            }
            if(itemtype === "gender")
            {
                if(this.quotaGenders.length === 0)
                {
                    console.log('Call made to Gender API');
                    var gendersList = [{"id":1,"name":"Male","selected":true},{"id":2,"name":"Female","selected":true}];
                    this.quotaGenders = gendersList;
                }
            }
        },
        selectQuotaCondition(event){
            console.log(event.target.value.toLowerCase());
            this.GetQuota(event.target.value.toLowerCase())
            this.showQuotaCondition = event.target.value.toLowerCase();
            this.showSubPopup = true;
            
        },
        SaveQuota(itemtype, taId, quotaid){
            var project = useProjectStore().project;
            for (var i = 0; i < project.projectTargetAudiences.length; i++)
            {
                var quota = {};
                if(project.projectTargetAudiences[i].id === taId)
                {
                    quota = {
                        "id": quotaid,
                        "name": this.currentQuota.name,
                        "fieldTarget": this.currentQuota.fieldTarget,
                        "status": this.currentQuota.status,
                        "completes": this.currentQuota.completes,
                        "prescreence":this.currentQuota.prescreence ,
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