import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 

export const useQualificationStore = defineStore('qualification', {
    state: () => ({
        currAgeRange: '',
        minAge: 0,
        maxAge: 0,
        countries: [],
        genders: [],
        onlinebanking:[],
        fieldofexperties:[],
        error: null,
        EditQualificationflag : true,
        selectedQuestionID:'',
        selectedQualification:[],
        countriesforvariable:[],
        bindname:'',
        variable:'',
        arrayvariableforcountry:[],
        arrayvariableforgender:[],
        genderslistforvariable:[],
        selectedQualificationfordisplay : false
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
                                this.variable=this.minAge+ ' - ' + this.maxAge ;
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
                                this.arrayvariableforcountry=project.projectTargetAudiences[i].qualifications[j].question.variables;
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
                                this.arrayvariableforgender=project.projectTargetAudiences[i].qualifications[j].question.variables ;
                                break;
                            }
                        }
                    }
                }
            }
        },
        SaveOnlineBankingQualification(itemtype, taId, qid) {
            console.log("Save called and values passed: " + itemtype + '=' + taId + '-' + qid);
            if(itemtype === 'online banking')
            {
                var project = useProjectStore().Qualificationlist;
                for (var i = 0; i < project.length; i++)
                {
                if(project[i].id === qid)
                            {
                                var variables = [];
                                var newGendersList = this.onlinebanking.filter(x => x.selected);
                                for(var k = 0; k < newGendersList.length; k++)
                                {
                                    variables.push({ "id": newGendersList[k].id, "name": newGendersList[k].name })
                                } project[i].question.variables = variables;
                                this.selectedQualification = project[i].question;

                                this.selectedQualificationfordisplay = true
                                 break;
                            }
                }
            }
        },
        SaveFieldofexpertiseQualification(itemtype, taId, qid) {
            console.log("Save called and values passed: " + itemtype + '=' + taId + '-' + qid);
            if(itemtype === 'field of expertise')
            {
                var project = useProjectStore().Qualificationlist;
                for (var i = 0; i < project.length; i++)
                {
                if(project[i].id === qid)
                            {
                                var variables = [];
                                var newGendersList = this.fieldofexperties.filter(x => x.selected);
                                for(var k = 0; k < newGendersList.length; k++)
                                {
                                    variables.push({ "id": newGendersList[k].id, "name": newGendersList[k].name })
                                } project[i].question.variables = variables;
                                this.selectedQualification = project[i].question;

                                this.selectedQualificationfordisplay = true 
                                 break;
                            }
                }
            }
        },
        GetAllCountries() {
            var countriesList = [{"id":1,"name":"UK","selected":true},{"id":2,"name":"Sweden","selected":false},{"id":3,"name":"Germany","selected":false},{"id":4,"name":"Denmark","selected":false},{"id":5,"name":"Finland","selected":false},{"id":6,"name":"Norway","selected":false},{"id":9,"name":"Spain","selected":false},{"id":10,"name":"Italy","selected":false},{"id":11,"name":"Hungary","selected":false},{"id":12,"name":"Greece","selected":false},{"id":22,"name":"USA","selected":false}];
            this.countriesforvariable = countriesList;
           
        },
        GetAllGenders() {
            var gendersList = [{"id":1,"name":"Male","selected":true},{"id":2,"name":"Female","selected":true}];
            this.genderslistforvariable = gendersList;
           
        },
        GetAllonlinebankinglist() {
            var onlinebankinglist = [
                {
                    "id": 16508,
                    "name": "Yes"
                },
                {
                    "id": 16509,
                    "name": "No"
                }
            ];
            this.onlinebanking = onlinebankinglist;
           
        },
        GetAllfieldofexpertieslist() {
             var fieldofexpertieslist =  [
                {
                    "id": 16446,
                    "name": "Administration"
                },
                {
                    "id": 16447,
                    "name": "Personnel/HR"
                },
                {
                    "id": 16449,
                    "name": "IT/Development"
                },
                {
                    "id": 16451,
                    "name": "Production"
                },
                {
                    "id": 16452,
                    "name": "Management"
                },
                {
                    "id": 16453,
                    "name": "Other"
                },
                {
                    "id": 22092,
                    "name": "Not Applicable"
                }
            ];
            this.fieldofexperties = fieldofexpertieslist;
           
        },
        
        /*Sa
        /*SaveCountries(itemtype, taId, qid) {
            return countriesList; 
        },*/
        GetQualification(itemtype, taId, qid) {
            if(itemtype === 'age' )
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
                    var countriesList = [{"id":1,"name":"UK","selected":false},{"id":2,"name":"Sweden","selected":false},{"id":3,"name":"Germany","selected":false},{"id":4,"name":"Denmark","selected":false},{"id":5,"name":"Finland","selected":false},{"id":6,"name":"Norway","selected":false},{"id":9,"name":"Spain","selected":false},{"id":10,"name":"Italy","selected":false},{"id":11,"name":"Hungary","selected":false},{"id":12,"name":"Greece","selected":false},{"id":22,"name":"USA","selected":false}];
                    this.countries = countriesList;
                }
            }
            if(itemtype === "gender")
            {
                if(this.genders.length === 0)
                {
                    console.log('Call made to Gender API');
                    var gendersList = [{"id":1,"name":"Male","selected":false},{"id":2,"name":"Female","selected":false}];
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
            if(itemtype === "online banking")
            {
                if(this.onlinebanking.length === 0)
                {
                    var onlinebankinglist = [
                        {
                            "id": 16508,
                            "name": "Yes"
                        },
                        {
                            "id": 16509,
                            "name": "No"
                        }
                    ];
                    this.onlinebanking = onlinebankinglist;
                }
            }
            if(itemtype === "field of expertise")
            {
                if(this.fieldofexperties.length === 0)
                {
                    var fieldofexpertieslist =  [
                        {
                            "id": 16446,
                            "name": "Administration"
                        },
                        {
                            "id": 16447,
                            "name": "Personnel/HR"
                        },
                        {
                            "id": 16449,
                            "name": "IT/Development"
                        },
                        {
                            "id": 16451,
                            "name": "Production"
                        },
                        {
                            "id": 16452,
                            "name": "Management"
                        },
                        {
                            "id": 16453,
                            "name": "Other"
                        },
                        {
                            "id": 22092,
                            "name": "Not Applicable"
                        }
                    ];
                    this.fieldofexperties = fieldofexpertieslist;
                }
            }
        },
        EditQualification(qid,taid,itemtype){

            this.EditQualificationflag = false
            console.log("EditQualification---> "+this.EditQualificationflag)
            this.selectedQuestionID=qid
        this.selectedQualification= this.GetQualificationwithID(qid,taid,itemtype);
            
        },
        EditQualificationback(){
            this.EditQualificationflag = true
            console.log("EditQualificationback--->"+this.EditQualificationflag)
            
        },
        GetQualificationwithID(itemtype,qid,taid)
        {
            // var project = useProjectStore().project;
            //     for (var i = 0; i < project.projectTargetAudiences.length; i++)
            //     {
            //         if(project.projectTargetAudiences[i].id === taid)
            //         {
            //             for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
            //             {
            //                 if(project.projectTargetAudiences[i].qualifications[j].question.id === qid)
            //                 {
            //                     var variable = project.projectTargetAudiences[i].qualifications[j];
            //                     this.selectedQualification = variable;
            //                     // if(this.selectedQualification.name.toLowerCase()==="country"){
            //                     //     this.GetQualification(this.selectedQualification.name.toLowerCase())
            //                     //     this.selectedQualification.variables= this.countries
            //                     // }
            //                     if(this.selectedQualification.question.name.toLowerCase()==="country"){
            //                         this.GetAllCountries()
            //                         this.selectedQualification.question.variables= this.countriesforvariable
            //                     }
            //                     if(this.selectedQualification.question.name.toLowerCase()==="gender"){
            //                         this.GetAllGenders()
            //                         this.selectedQualification.question.variables= this.genderslistforvariable
            //                     }
                               
            //                     this.selectedQualification.push(variable)
            //                     break;

            //                 }
            //             }
            //         }
            //     }
            
            var project = useProjectStore().Qualificationlist;
            for (var i = 0; i < project.length; i++){
                if(project[i].question.id===qid){
                    var variable = project[i];
                    this.selectedQualification = variable;
                    if(this.selectedQualification.question.name.toLowerCase()==="online banking"){
                      this.GetAllonlinebankinglist()
                    this.selectedQualification.question.variables= this.onlinebanking
                    }
                    if(this.selectedQualification.question.name.toLowerCase()==="field of expertise"){
                        this.GetAllfieldofexpertieslist()
                        this.selectedQualification.question.variables= this.fieldofexperties
                        }
                        
                    this.selectedQualification.push(variable)
                     console.log("selectedQualification--->"+this.variable);
                     
                    break;
                }
            }
                
             

        }
    }
})