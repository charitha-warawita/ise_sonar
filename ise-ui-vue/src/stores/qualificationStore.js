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
        EditQualificationflag : true,
        selectedQuestionID:'',
        selectedQualification:[],
        countriesforvariable:[],
        bindname:'',
        variable:'',
        arrayvariableforcountry:[],
        arrayvariableforgender:[],
        genderslistforvariable:[],
        selectedQualificationfordisplay : false,

        iseUrl: import.meta.env.VITE_ISE_API_URL,
        iseCountryPath: import.meta.env.VITE_ISE_API_COUNTRIES,
        iseProfCatPath: import.meta.env.VITE_ISE_API_PROFILECATEGORIES,
        iseQAByCategoryPath: import.meta.env.VITE_ISE_API_QABYCATEGORY
    }),
    getters: {

    },
    actions: {
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
        },
        async GetQualificationAge(itemtype, taId, qid)
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
                            this.minAge = variable.name.substring(0, numberIndex);
                            this.maxAge = variable.name.substring(numberIndex+3, variable.name.length);
                            break;
                        }
                    }
                }
            }
        },
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
        async GetQualificationCountry(itemtype, taId, qid)
        {
            if(this.countries.length === 0)
                {
                    // var countriesList = [{"id":1,"name":"UK","selected":true},{"id":2,"name":"Sweden","selected":false},{"id":3,"name":"Germany","selected":false},{"id":4,"name":"Denmark","selected":false},{"id":5,"name":"Finland","selected":false},{"id":6,"name":"Norway","selected":false},{"id":9,"name":"Spain","selected":false},{"id":10,"name":"Italy","selected":false},{"id":11,"name":"Hungary","selected":false},{"id":12,"name":"Greece","selected":false},{"id":22,"name":"USA","selected":false}];
                    // this.countries = countriesList;
                    this.countriesLoading = true
                    try {
                        this.countries = await fetch(this.iseUrl + this.iseCountryPath)
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
        async UpdateCountrySelection(itemType, taId, qualificationId, country) {
            if(this.countries.length > 0)
            {
                for(var i = 0; i < this.countries.length; i++)
                {
                    if(this.countries[i].id === country.id)
                        this.countries[i].selected = true;
                    else
                        this.countries[i].selected = false; 
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
        async GetQualificationGender(itemtype, taId, qid)
        {
            if(this.genders.length === 0)
            {
                console.log('Call made to Gender API');
                var gendersList = [{"id":1,"name":"Male","selected":false},{"id":2,"name":"Female","selected":false}];
                this.genders = gendersList;
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
        async GetProfileCategories(taId)
        {
            this.currentTAId = taId;
            if(this.profCategories.length === 0)
            {
                this.profCategoriesLoading = true;
                try {
                     var currProfCategories = await fetch(this.iseUrl + this.iseProfCatPath)
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
                        
                        this.profCategories.push({ name: currName, count: currProfCategoryCount, selected: currSelected, qas: [] });
                    }
                } catch (error) {
                    this.profCategoriesError = error;
                } finally {
                    this.profCategoriesLoading = false;
                }
            }
        },
        async GetQandAForCategory(profCategory)
        {
            var category = profCategory.name
            this.selectedCategory = category;
            this.profCategoriesLoading = true;

            try {
                var currIndex = this.profCategories.findIndex(x => x.name === category)
                if(this.profCategories[currIndex].qas.length < 1)
                {
                    var qas = await fetch(this.iseUrl + this.iseQAByCategoryPath + encodeURIComponent(category))
                    .then((response) => response.json())

                    for(var i =0; i < qas.length; i++)
                    {
                        for(var j = 0; j< qas[i].variables.length; j++)
                        {
                            qas[i].variables[j].selected = false;
                        }
                    }
                    
                    this.profCategories[currIndex].qas = qas;
                }

                this.categoryQAs = this.profCategories[currIndex].qas;

            } catch (error) {
                this.profCategoriesError = error;
            } finally {
                this.profCategoriesLoading = false;
            }
        },
        async SaveQAToProject(question, answerId, answerName) {
            var isAddingNewElement = true;

            //Updating UI
            if(this.profCategories.length > 0)
            {
                var index = this.profCategories.findIndex(x => x.name === question.categoryName);
                
                if(this.profCategories[index].qas.length > 0)
                {
                    var currIndex = this.profCategories[index].qas.findIndex(x => x.id === question.id);
                    if(currIndex > -1)
                    {
                        var currVarIndex = this.profCategories[index].qas[currIndex].variables.findIndex(x => x.id === answerId);
                        if(currVarIndex > -1)
                        {
                            if(this.profCategories[index].qas[currIndex].variables[currVarIndex].selected)
                                isAddingNewElement = false;
                                this.profCategories[index].qas[currIndex].variables[currVarIndex].selected = !this.categoryQAs[currIndex].variables[currVarIndex].selected
                        }
                    }
                    this.categoryQAs = this.profCategories[index].qas;
                } 

                if(isAddingNewElement) {
                    this.profCategories[index].selected = true;
                    this.profCategories[index].count = this.profCategories[index].count+1;
                }
                else
                {
                    this.profCategories[index].count = this.profCategories[index].count-1;
                    if(this.profCategories[index].count < 1)
                    this.profCategories[index].selected = false;
                }
            }

            //Updating project Model
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
                        else {
                            // We need to remove the item
                            if(!isAddingNewElement)
                            {
                                if(project.projectTargetAudiences[i].qualifications[qualQuestionIndex].question.variables.length === 1)
                                {
                                    project.projectTargetAudiences[i].qualifications.splice(qualQuestionIndex, 1);
                                }
                                else
                                {
                                    project.projectTargetAudiences[i].qualifications[qualQuestionIndex].question.variables.splice(qualVarIndex, 1);
                                }
                            }
                        }
                    }
                }
            }
        },
        UpdateQualLogOperation(taId, qid, ld)
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
                            project.projectTargetAudiences[i].qualifications[j].logicalDecision = ld;
                        }
                    }
                }
            }
        },
        sortOrderforQual(added, taId) 
        {
            if (added.item) {
                var project = useProjectStore().project;
                for (var i = 0; i < project.projectTargetAudiences.length; i++)
                {
                    if(project.projectTargetAudiences[i].id === taId)
                    {
                        var currQual = project.projectTargetAudiences[i].qualifications;
                        var currIndex = added.oldIndex; var newIndex = added.newIndex;

                        var tmp = currQual[currIndex];
                        currQual.splice(currIndex, 1);
                        currQual.splice(newIndex, 0, tmp);

                        for(var j = 0; j < currQual.length; j++)
                        {
                            currQual[j].order = j+1;
                        }

                        project.projectTargetAudiences[i].qualifications = currQual;
                        break;
                    }
                }
            }
        }
    }
})