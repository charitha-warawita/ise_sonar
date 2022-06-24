import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 

export const useQualificationStore = defineStore('qualification', {
    state: () => ({
        currAgeRange: '',
        minAge: 0,
        maxAge: 0
    }),
    getters: {

    },
    actions: {
        SaveQualification(itemtype, taId, qid) {
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
                            }
                        }
                    }
                }
            }
        },
        GetQualification(itemtype, taId, qid) {
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

                            }
                        }
                    }
                }
            }
        }
    }
})