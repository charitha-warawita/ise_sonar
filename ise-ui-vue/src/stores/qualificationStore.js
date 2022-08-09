import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 

export const useQualificationStore = defineStore('qualification', {
    state: () => ({
        currentTAId: 0,
        currentQAId: 0,

        profCategories: [],
        profCategoriesLoading: false,
        profCategoriesError: null,
        categoryQAs: [],
        selectedCategory: '',
        error: null,

        iseUrl: import.meta.env.VITE_ISE_API_URL,
        iseCountryPath: import.meta.env.VITE_ISE_API_COUNTRIES,
        iseProfCatPath: import.meta.env.VITE_ISE_API_PROFILECATEGORIES,
        iseQAByCategoryPath: import.meta.env.VITE_ISE_API_QABYCATEGORY
    }),
    actions: {
        async GetProfileCategories(taId)
        {
            this.selectedCategory=''; this.categoryQAs=[]; this.profCategories=[];
            this.currentTAId = taId;
            try {
                this.profCategoriesLoading = true;
                var currProfCategories = await fetch(this.iseUrl + this.iseProfCatPath)
                    .then((response) => response.json());

                for(var i = 0; i < currProfCategories.length; i++)
                {
                    var currName = currProfCategories[i];
                    this.profCategories.push({ name: currName, count: 0, selected: false, qas: [] });
                }
            } catch (error) {
                this.profCategoriesError = error;
            } finally {
                this.profCategoriesLoading = false;
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
                            
                            this.profCategories[index].qas[currIndex].variables[currVarIndex].selected 
                                            = !this.profCategories[index].qas[currIndex].variables[currVarIndex].selected;
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
        },
        saveQualificationDetailstoProject()
        {
            var project = useProjectStore().project;
            try {
                    var taIndex = project.targetAudiences.findIndex(x => x.tempId === this.currentTAId);
                    if(taIndex > -1) {
                        for(var i = 0; i < this.profCategories.length; i++) {
                            if(this.profCategories[i].selected) {
                                for(var j = 0; j < this.profCategories[i].qas.length; j++)
                                {
                                    if(this.profCategories[i].qas[j].variables.some(e => e.selected === true)) {
                                        var currentLength = project.targetAudiences[taIndex].qualifications.length;
                                        var currentqas = this.profCategories[i].qas[j];
                                        project.targetAudiences[taIndex].qualifications.push(
                                            {
                                                "tempId": currentLength + 1, "order": currentLength + 1, 
                                                "logicalDecision": "AND", "NumberOfRequiredConditions": 0,
                                                "IsActive": true,
                                                "question": {
                                                    "id":  currentqas.id, "name": currentqas.name, "text": currentqas.text, "categoryName": currentqas.categoryName,
                                                    "variables": currentqas.variables.filter(e => e.selected === true)
                                                } 
                                            }
                                        )
                                    }
                                }
                            }
                        }
                    }
                }
                catch (error) 
                {
                    this.error = error
                }
        },
        UpdateQualLogOperation(taId, qid, ld)
        {
            var project = useProjectStore().project;
            for (var i = 0; i < project.targetAudiences.length; i++)
            {
                if(project.targetAudiences[i].tempId === taId)
                {
                    for(var j = 0; j < project.targetAudiences[i].qualifications.length; j++)
                    {
                        if(project.targetAudiences[i].qualifications[j].tempId === qid)
                        {
                            project.targetAudiences[i].qualifications[j].logicalDecision = ld;
                        }
                    }
                }
            }
        },
        sortOrderforQual(added, taId) 
        {
            if (added.item) {
                var project = useProjectStore().project;
                for (var i = 0; i < project.targetAudiences.length; i++)
                {
                    if(project.targetAudiences[i].tempId === taId)
                    {
                        var currQual = project.targetAudiences[i].qualifications;
                        var currIndex = added.oldIndex; var newIndex = added.newIndex;

                        var tmp = currQual[currIndex];
                        currQual.splice(currIndex, 1);
                        currQual.splice(newIndex, 0, tmp);

                        for(var j = 0; j < currQual.length; j++)
                        {
                            currQual[j].order = j+1;
                        }

                        project.targetAudiences[i].qualifications = currQual;
                        break;
                    }
                }
            }
        },
        RemoveQualification(taId, qualId) {
            var project = useProjectStore().project;
            var taIndex = project.targetAudiences.findIndex(x => x.tempId === taId);
            if(taIndex > -1) {
                var qualIndex = project.targetAudiences[taIndex].qualifications.findIndex(x => x.tempId === qualId)
                if(qualIndex > -1)
                project.targetAudiences[taIndex].qualifications.splice(qualIndex,1);

                for(var j = 0; j < project.targetAudiences[taIndex].qualifications.length; j++)
                {
                    project.targetAudiences[taIndex].qualifications[j].order = j+1;
                }
            }
        }
    }
})