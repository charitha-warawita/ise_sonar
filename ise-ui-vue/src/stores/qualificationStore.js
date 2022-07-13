import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 

export const useQualificationStore = defineStore('qualification', {
    state: () => ({
        currentTAId: 0,
        currentQAId: 0,
        
        currAgeRange: '',
        minAge: 0,
        maxAge: 0,

        countries: [],
        countriesLoading: false,
        countriesError: null,

        genders: [],

        profCategories: [],
        profCategoriesLoading: false,
        profCategoriesError: null,
        categoryQAs: [],
        selectedCategory: '',

        onlinebanking:[],
        fieldofexperties:[],
        error: null,
        EditQualificationflag:true,
        selectedQuestionID:'',
        selectedQualification:[],
        countriesforvariable:[],
        bindname:'',
        variable:'',
        arrayvariableforcountry:[],
        arrayvariableforgender:[],
        genderslistforvariable:[]
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
                                // this.arrayvariableforcountry=project.projectTargetAudiences[i].qualifications[j].question.variables;
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
        async SaveQAToProject(question, answerId, answerName) {
            if(this.profCategories.length > 0)
            {
                var currIndex = this.profCategories.findIndex(x => x.name === question.categoryName);
                this.profCategories[currIndex].selected = true;
                this.profCategories[currIndex].count = this.profCategories[currIndex].count+1;
            }
            if(this.categoryQAs.length > 0)
            {
                var currIndex = this.categoryQAs.findIndex(x => x.id === question.id);
                if(currIndex > -1)
                {
                    var currVarIndex = this.categoryQAs[currIndex].variables.findIndex(x => x.id === answerId);
                    if(currVarIndex > -1)
                    {
                        this.categoryQAs[currIndex].variables[currVarIndex].selected = true;
                    }
                }
            }

            var project = useProjectStore().project;
            for (var i = 0; i < project.projectTargetAudiences.length; i++)
            {
                if(project.projectTargetAudiences[i].id === this.currentTAId)
                {
                    var currentLength = project.projectTargetAudiences[i].qualifications.length;
                    var qualQuestionIndex = project.projectTargetAudiences[i].qualifications.findIndex(x => x.question.id === question.id)
                    if(qualQuestionIndex < 0)
                    {
                        var qualification = {
                            "id": currentLength + 1, "order": currentLength + 1, 
                            "logicalDecision": "OR", "NumberOfRequiredConditions": 0,
                            "IsActive": true,
                            "question": {
                                "id": question.id, "name": question.name, "text": question.text, "categoryName": question.categoryName,
                                "variables": [{ "id": answerId, "name": answerName }]
                            } 
                        }
                        project.projectTargetAudiences[i].qualifications.push(qualification)
                    }
                    else {
                        var qualVarIndex = project.projectTargetAudiences[i].qualifications[qualQuestionIndex].question.variables.findIndex(x => x.id === answerId)
                        if(qualVarIndex < 0)
                        {
                            project.projectTargetAudiences[i].qualifications[qualQuestionIndex].question.variables.push({ "id": answerId, "name": answerName })
                        }
                    }
                }
            }
        },
        /*SaveOnlineBankingQualification(itemtype, taId, qid) {
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
           
        },*/
        
        /*Sa
        /*SaveCountries(itemtype, taId, qid) {
            return countriesList; 
        },*/
        async GetQualificationAge(itemtype, taId, qid)
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
                            var numberIndex = variable.name.indexOf(' - ');
                            this.minAge = variable.name.substring(0, numberIndex);
                            this.maxAge = variable.name.substring(numberIndex+3, variable.name.length);
                            break;
                        }
                    }
                }
            }
        },
        async GetProfileCategories(taId, qId)
        {
            this.currentQAId = qId; this.currentTAId = taId;
            if(this.profCategories.length === 0)
            {
                this.profCategoriesLoading = true;
                try {
                     var currProfCategories = await fetch('http://localhost:5197/api/Reference/project/profilecategories')
                    .then((response) => response.json());

                    var projectsCurrProfCategories = [];
                    var project = useProjectStore().project;
                    for (var i = 0; i < project.projectTargetAudiences.length; i++)
                    {
                        if(project.projectTargetAudiences[i].id === taId)
                        {
                            for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
                            {
                                var q = project.projectTargetAudiences[i].qualifications[j];
                                projectsCurrProfCategories.push(q.question.categoryName);
                            }
                        }
                    }
                    for(var i = 0; i < currProfCategories.length; i++)
                    {
                        var currName = currProfCategories[i];
                        var currProfCategoryCount = projectsCurrProfCategories.filter(x => x === currProfCategories[i]).length;
                        var currSelected = false;
                        if(currProfCategoryCount > 0)
                            currSelected = true;
                        
                        this.profCategories.push({ name: currName, count: currProfCategoryCount, selected: currSelected });
                    }
                } catch (error) {
                    this.profCategoriesError = error;
                } finally {
                    this.profCategoriesLoading = false;
                }
            }
        },
        async GetQandAForCategory(category)
        {
            this.selectedCategory = category;
            this.profCategoriesLoading = true;

            try {
                var qas = await fetch('http://localhost:5197/api/Reference/project/questions?category=' + encodeURIComponent(category))
                .then((response) => response.json())

                for(var i =0; i < qas.length; i++)
                {
                    for(var j = 0; j< qas[i].variables.length; j++)
                    {
                        qas[i].variables[j].selected = false;
                    }
                }
                this.categoryQAs = qas;
            } catch (error) {
                this.profCategoriesError = error;
            } finally {
                this.profCategoriesLoading = false;
            }
        },
        async GetQualificationCountry(itemtype, taId, qid)
        {
            if(this.countries.length === 0)
                {
                    // var countriesList = [{"id":1,"name":"UK","selected":true},{"id":2,"name":"Sweden","selected":false},{"id":3,"name":"Germany","selected":false},{"id":4,"name":"Denmark","selected":false},{"id":5,"name":"Finland","selected":false},{"id":6,"name":"Norway","selected":false},{"id":9,"name":"Spain","selected":false},{"id":10,"name":"Italy","selected":false},{"id":11,"name":"Hungary","selected":false},{"id":12,"name":"Greece","selected":false},{"id":22,"name":"USA","selected":false}];
                    // this.countries = countriesList;
                    this.countriesLoading = true
                    try {
                        this.countries = await fetch('http://localhost:5197/api/Reference/project/countries')
                        .then((response) => response.json())
                    } catch (error) {
                        this.countriesError = error
                        return;
                    } finally {
                        this.countriesLoading = false
                    }
                }
                var currProjCountryIdList = [];
                var project = useProjectStore().project;
                for (var i = 0; i < project.projectTargetAudiences.length; i++)
                {
                    if(project.projectTargetAudiences[i].id === taId)
                    {
                        for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
                        {
                            if(project.projectTargetAudiences[i].qualifications[j].id === qid)
                            {
                                var currVar = project.projectTargetAudiences[i].qualifications[j].question.variables;
                                for(var k = 0; k < currVar.length; k++)
                                    currProjCountryIdList.push(currVar[k].id);
                            }
                        }
                    }
                }
                for (var i = 0; i < this.countries.length; i++)
                {
                    if(currProjCountryIdList.includes(this.countries[i].id))
                        this.countries[i].selected = true;
                    else 
                        this.countries[i].selected = false;
                }
        },
        async GetQualificationGender(itemtype, taId, qid)
        {
            if(this.genders.length === 0)
            {
                console.log('Call made to Gender API');
                var gendersList = [{"id":1,"name":"Male","selected":false},{"id":2,"name":"Female","selected":false}];
                this.genders = gendersList;
            }
        },

        async GetQualification(itemtype, taId, qid) {
            if(itemtype === 'age' )
            {
                this.GetQualificationAge(itemtype,taId, qid);
            }
            if(itemtype === "country")
            {
                this.GetQualificationCountry(itemtype, taId, qid);
            }
            if(itemtype === "gender")
            {
                this.GetQualificationGender(itemtype, taId, qid);
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
            this.selectedQuestionID=qid
           this.selectedQualification= this.GetQualificationwithID(qid,taid,itemtype);
            
        },
        EditQualificationback(){
            this.EditQualificationflag = true
            
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
                   // this.GetQualification(itemtype,taid,qid)
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
                    
                    break;
                }
            }
                
             

        }
    }
})