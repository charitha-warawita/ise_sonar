import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 

export const useQualificationStore = defineStore('qualification', {
    state: () => ({
        currAgeRange: '',
        minAge: 0,
        maxAge: 0,
        countries: [],
        genders: [],
        error: null
    }),
    getters: {

    },
    actions: {
        SaveAgeQualification(itemtype, taId, qid) {
            console.log("Save called and values passed: " + itemtype + '=' + taId + '-' + qid);
            if(itemtype === 'age')
            {
                var project = useProjectStore().project;
                for (var i = 0; i < project.projectTargetAudiences.length; i++)
                {
                    if(project.projectTargetAudiences[i].id === taId)
                    {
                        for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
                        {
                            if(project.projectTargetAudiences[i].qualifications[j].id === qid)
                            {
                                project.projectTargetAudiences[i].qualifications[j].question.variables[0].name = this.minAge + ' - ' + this.maxAge;
                                break;
                            }
                        }
                    }
                }
            }
        },
        SaveCountryQualification(itemtype, taId, qid) {
            console.log("Save called and values passed: " + itemtype + '=' + taId + '-' + qid);
            if(itemtype === 'country')
            {
                var project = useProjectStore().project;
                for (var i = 0; i < project.projectTargetAudiences.length; i++)
                {
                    if(project.projectTargetAudiences[i].id === taId)
                    {
                        for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
                        {
                            if(project.projectTargetAudiences[i].qualifications[j].id === qid)
                            {
                                var variables = [];
                                var newCountriesList = this.countries.filter(x => x.selected);
                                for(var k = 0; k < newCountriesList.length; k++)
                                {
                                    variables.push({ "id": newCountriesList[k].id, "name": newCountriesList[k].name })
                                }
                                
                                project.projectTargetAudiences[i].qualifications[j].question.variables = variables;
                                break;
                            }
                        }
                    }
                }
            }
        },
        SaveGenderQualification(itemtype, taId, qid) {
            console.log("Save called and values passed: " + itemtype + '=' + taId + '-' + qid);
            if(itemtype === 'gender')
            {
                var project = useProjectStore().project;
                for (var i = 0; i < project.projectTargetAudiences.length; i++)
                {
                    if(project.projectTargetAudiences[i].id === taId)
                    {
                        for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
                        {
                            if(project.projectTargetAudiences[i].qualifications[j].id === qid)
                            {
                                var variables = [];
                                var newGendersList = this.genders.filter(x => x.selected);
                                for(var k = 0; k < newGendersList.length; k++)
                                {
                                    variables.push({ "id": newGendersList[k].id, "name": newGendersList[k].name })
                                }
                                
                                project.projectTargetAudiences[i].qualifications[j].question.variables = variables;
                                break;
                            }
                        }
                    }
                }
            }
        },
        /*GetAllCountries(itemtype, taId, qid) {
            var countriesList = [{"id":1,"name":"UK","selected":true},{"id":2,"name":"Sweden","selected":false},{"id":3,"name":"Germany","selected":false},{"id":4,"name":"Denmark","selected":false},{"id":5,"name":"Finland","selected":false},{"id":6,"name":"Norway","selected":false},{"id":9,"name":"Spain","selected":false},{"id":10,"name":"Italy","selected":false},{"id":11,"name":"Hungary","selected":false},{"id":12,"name":"Greece","selected":false},{"id":22,"name":"USA","selected":false}];
            this.countries = countriesList;
            return this.countries; 
        },*/
        /*SaveCountries(itemtype, taId, qid) {
            return countriesList; 
        },*/
      async  GetQualification(itemtype, taId, qid) {
            if(itemtype === 'age')
            {
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
                                break;
                            }
                        }
                    }
                }
            }
            if(itemtype === "country")
            {
                if(this.countries.length === 0)
                {
                    var countriesList = [{"id":1,"name":"UK","selected":true},{"id":2,"name":"Sweden","selected":false},{"id":3,"name":"Germany","selected":false},{"id":4,"name":"Denmark","selected":false},{"id":5,"name":"Finland","selected":false},{"id":6,"name":"Norway","selected":false},{"id":9,"name":"Spain","selected":false},{"id":10,"name":"Italy","selected":false},{"id":11,"name":"Hungary","selected":false},{"id":12,"name":"Greece","selected":false},{"id":22,"name":"USA","selected":false}];
                    this.countries = countriesList;
                }
            }
            if(itemtype === "gender")
            {
                if(this.genders.length === 0)
                {
                    console.log('Call made to Gender API');
                    var gendersList = [{"id":1,"name":"Male","selected":true},{"id":2,"name":"Female","selected":true}];
                    this.genders = gendersList;
                    // var gendersList=[]
                    
                    /*try {
                         this.gendersList = await fetch('https://api.cintworks.net/ordering/Reference/Genders')   
                         .then((response) => response.json())
                         console.log('genders: ' + JSON.stringify(gendersList));
                         this.genders = gendersList;
                    }
                    catch(error) {
                        this.error= error
                    }*/
                }
            }
        }
    }
})