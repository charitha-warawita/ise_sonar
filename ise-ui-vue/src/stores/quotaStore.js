import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 
export const useQuotaStore = defineStore('quota', {
    state: () => ({
        conditionlist:[],
        currAgeRange: '',
        quotaMinAge: 0,
        quotaMaxAge:0,
        error: null,
        showQuotaCondition:'',
        quotaCountries:[],
        quotaGenders:[],
        showSubPopup: false,
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
                showQuotaCondition:'',
                quotaCountries:[],
                quotaMinAge:0,
                quotaMaxAge:0,
                ServeyQuotaType:["Client","Control","Supplier"],
            };
            this.showSubPopup = false;
        },
        GetQuota(itemtype, taId, qid) {
            if(itemtype === 'age')
            {
                this.GetQuotanAge(itemtype,taId, qid);
                // var project = useProjectStore().project;
                // for (var i = 0; i < project.projectTargetAudiences.length; i++)
                // {
                //     var projectQuotas=project.projectTargetAudiences
                //     if(project.projectTargetAudiences[i].id === taId)
                //     {
                //         for(var j = 0; j < project.projectTargetAudiences[i].quota.length; j++)
                //         {
                //             if(project.projectTargetAudiences[i].quota[j].id === qid)
                //             {
                //                 var variable = project.projectTargetAudiences[i].quota[j].condition.variables[0];
                //                 this.currAgeRange = variable.name;
                //                 break;
                //             }
                //         }
                //     }
               // }
            }
            if(itemtype === "country")
            {
                this.GetQuotaCountry(itemtype, taId, qid);
                // if(this.quotaCountries.length === 0)
                // {
                //     var countriesList = [{"id":1,"name":"UK","selected":true},{"id":2,"name":"Sweden","selected":false},{"id":3,"name":"Germany","selected":false},{"id":4,"name":"Denmark","selected":false},{"id":5,"name":"Finland","selected":false},{"id":6,"name":"Norway","selected":false},{"id":9,"name":"Spain","selected":false},{"id":10,"name":"Italy","selected":false},{"id":11,"name":"Hungary","selected":false},{"id":12,"name":"Greece","selected":false},{"id":22,"name":"USA","selected":false}];
                //     this.quotaCountries = countriesList;
                // }
            }
            if(itemtype === "gender")
            {
                this.GetQuotaGender(itemtype, taId, qid);
                // if(this.quotaGenders.length === 0)
                // {
                //     console.log('Call made to Gender API');
                //     var gendersList = [{"id":1,"name":"Male","selected":true},{"id":2,"name":"Female","selected":true}];
                //     this.quotaGenders = gendersList;
                // }
            }
        },
        async GetQuotaCountry(itemtype, taId, qid)
        { 
            if(this.quotaCountries.length === 0)
                {
                    console.log(itemtype + '--' + taId + '--' + qid);
                    // var countriesList = [{"id":1,"name":"UK","selected":true},{"id":2,"name":"Sweden","selected":false},{"id":3,"name":"Germany","selected":false},{"id":4,"name":"Denmark","selected":false},{"id":5,"name":"Finland","selected":false},{"id":6,"name":"Norway","selected":false},{"id":9,"name":"Spain","selected":false},{"id":10,"name":"Italy","selected":false},{"id":11,"name":"Hungary","selected":false},{"id":12,"name":"Greece","selected":false},{"id":22,"name":"USA","selected":false}];
                    // this.countries = countriesList;
                    //this.countriesLoading = true
                    try {
                        this.quotaCountries = await fetch(this.iseUrl + this.iseCountryPath)
                        .then((response) => response.json())
                    } catch (error) {
                        //this.countriesError = error
                        return;
                    } finally {
                        this.countriesLoading = false
                    }
                }
                // var currProjCountryIdList = [];
                // var project = useProjectStore().project;
                // for (var i = 0; i < project.projectTargetAudiences.length; i++)
                // {
                //     if(project.projectTargetAudiences[i].id === taId)
                //     {
                //         for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
                //         {
                //             if(project.projectTargetAudiences[i].qualifications[j].id === qid)
                //             {
                //                 var currVar = project.projectTargetAudiences[i].qualifications[j].question.variables;
                //                 for(var k = 0; k < currVar.length; k++)
                //                     currProjCountryIdList.push(currVar[k].id);
                //             }
                //         }
                //     }
                // }
                for (var i = 0; i < this.quotaCountries.length; i++)
                {
                    if(currProjCountryIdList.includes(this.quotaCountries[i].id))
                        this.quotaCountries[i].selected = true;
                    else 
                        this.quotaCountries[i].selected = false;
                }
        },
        async GetQuotanAge(itemtype, taId, qid)
        {
            console.log(itemtype + '--' + taId + '--' + qid);
            var project = useProjectStore().project;
            for (var i = 0; i < project.projectTargetAudiences.length; i++)
            {
                if(project.projectTargetAudiences[i].id === taId)
                {
                    for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
                    {
                        if(project.projectTargetAudiences[i].qualifications[j].id === qid)
                        {
                            var variable = project.projectTargetAudiences[i].qualifications[j].question.variables[0];
                            this.currAgeRange = variable.name;
                            var numberIndex = variable.name.indexOf(' - ');
                            this.quotaMinAge = variable.name.substring(0, numberIndex);
                            this.quotaMaxAge = variable.name.substring(numberIndex+3, variable.name.length);
                            break;
                        }
                    }
                }
            }
        },
        async GetQuotaGender(itemtype, taId, qid)
        {
            if(this.quotaGenders.length === 0)
            {
                console.log('Call made to Gender API');
                var gendersList = [{"id":1,"name":"Male","selected":false},{"id":2,"name":"Female","selected":false}];
                this.quotaGenders = gendersList;
            }
        },
        // async GetQandAForCategory(profCategory)
        // {
        //     var category = profCategory.name
        //     this.selectedCategory = category;
        //     this.profCategoriesLoading = true;

        //     try {
        //         var currIndex = this.profCategories.findIndex(x => x.name === category)
        //         if(this.profCategories[currIndex].qas.length < 1)
        //         {
        //             var qas = await fetch(this.iseUrl + this.iseQAByCategoryPath + encodeURIComponent(category))
        //             .then((response) => response.json())

        //             for(var i =0; i < qas.length; i++)
        //             {
        //                 for(var j = 0; j< qas[i].variables.length; j++)
        //                 {
        //                     qas[i].variables[j].selected = false;
        //                 }
        //             }
                    
        //             this.profCategories[currIndex].qas = qas;
        //         }

        //         this.categoryQAs = this.profCategories[currIndex].qas;

        //     } catch (error) {
        //         this.profCategoriesError = error;
        //     } finally {
        //         this.profCategoriesLoading = false;
        //     }
        // },
        serveyQuotaTyp(event){
            this.currentQuota.serveyQuotaTypeSelected = event.target.value;    
        },
        addCondition(){
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