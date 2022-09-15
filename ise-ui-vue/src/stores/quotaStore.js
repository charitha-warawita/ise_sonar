import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 
export const useQuotaStore = defineStore('quota', {
    state: () => ({
        enableSave: false,
        showConditionDetail:false,
        conditionlist:[],
        currentCondition: null,
        error: null,
        conditiongrid:false,
        currentQuota: {},
    }),
    actions: {
        LoadDefaultCurrentQuota() {
            this.currentQuota = {
                name: '',
                selectedServeyQuotaType:'Completed',
                selectedAdjustmentType:'',
                selectedIsActive:"True",
                fieldTargetPercentage: 0,
                fieldTargetNominal: 0,
                isActive: ["True", "False"],
                completes: 0,
                prescreens: 0,
                quotaPercentage: 0,
                quotaNominal: 0,
                conditions:[],
                ServeyQuotaType:["--Select--","Started","Completed"],
                adjustmentType:["--Select--", "Nominal","Percentage"],
                errors:[],
            };
            this.enableSave = false;
            this.conditionlist = [];
            this.currentCondition = null;
            this.showConditionDetail = false;
            this.conditiongrid=false;

        },
        isActiveState(event) {
            this.currentQuota.selectedIsActive = event.target.value.toLowerCase();
        },
        serveyQuotaTyp(event) {
            this.currentQuota.selectedServeyQuotaType = event.target.value.toLowerCase();    
        },
        adjustmentType(event){
            var selectedValue = event.target.value.toLowerCase();  
            this.currentQuota.selectedAdjustmentType = selectedValue
        },
        UpdateAdjValue(totalCompletes, element, key) {
            if(Number.isInteger(totalCompletes)) {
                if(element === 'fieldTarget') {
                    if(key === 'nominal')   //this.currentQuota.fieldTargetNominal > totalCompletes ? this.currentQuota.fieldTargetNominal / 100 : (this.currentQuota.fieldTargetNominal/totalCompletes) * 100;
                        this.currentQuota.fieldTargetPercentage = (this.currentQuota.fieldTargetNominal/totalCompletes) * 100;
                    else
                        this.currentQuota.fieldTargetNominal = totalCompletes* (this.currentQuota.fieldTargetPercentage/100);
                } else {
                    if(key === 'nominal')
                        this.currentQuota.quotaPercentage = (this.currentQuota.quotaNominal/totalCompletes) * 100;
                    else
                        this.currentQuota.quotaNominal = totalCompletes* (this.currentQuota.quotaPercentage/100);
                }
            }        
        },
        addCondition(taId){
            this.conditionlist = [];
            this.showConditionDetail =false;
            this.conditiongrid = true;
            this.enableSave = false;
            var project = useProjectStore().project;
            var index = project.targetAudiences.findIndex(x => x.tempId === taId)
            if(index > -1) {
                for(var i = 0; i < project.targetAudiences[index].qualifications.length; i++) {
                    this.conditionlist.push(JSON.parse(JSON.stringify(project.targetAudiences[index].qualifications[i])));
                }
            }
        },
        selectQuotaCondition(event){
            console.log(event.target.value.toLowerCase());
            var  selectedConditionId = parseInt(event.target.value);

            var con = this.conditionlist.find(x => x.order === selectedConditionId);
            this.currentCondition = con;
            for(var i =0; i < this.currentCondition.question.variables.length; i++)
                this.currentCondition.question.variables[i].selected = false;
            this.showConditionDetail =true
        },
        SelectQuotaConditionAnswer(answerId) {
            var index = this.currentCondition.question.variables.findIndex(x => x.id === answerId);
            this.currentCondition.question.variables[index].selected = !this.currentCondition.question.variables[index].selected;
        },
        GoToFirstSection() {
            this.conditiongrid = false;

            if(this.currentQuota.conditions === undefined || this.currentQuota.conditions.length < 1)
                this.enableSave = false;
            else
                this.enableSave = true;
            
        },
        SaveCondition() {
            if(this.currentCondition !== null) {
                this.currentCondition.question.variables = this.currentCondition.question.variables.filter(x => x.selected === true)
                this.currentQuota.conditions.push(this.currentCondition);
                this.currentCondition = null;
                this.conditiongrid = false;
                this.enableSave = true;
            }
        },
        RemoveCondition(conditionId) {
            var index = this.currentQuota.conditions.findIndex(x => x.tempId === conditionId);
            if(index > -1)
                this.currentQuota.conditions.splice(index, 1);

            if(this.currentQuota.conditions === undefined || this.currentQuota.conditions.length < 1)
                this.enableSave = false;
        },
        SaveQuota(taid){
            var project = useProjectStore().project;
            var index = project.targetAudiences.findIndex(x => x.tempId === taid)
            var quotaId = project.targetAudiences[index].quotas.length + 1;
            if(index > -1) {
                var quota = {
                    "tempId": quotaId,
                    "order": quotaId, 
                    "quotaName": this.currentQuota.name,
                    "quotaType": this.currentQuota.selectedServeyQuotaType,
                    "fieldTarget": this.currentQuota.fieldTargetNominal,
                    "limit": this.currentQuota.quotaNominal,
                    "prescreens": this.currentQuota.prescreens,
                    "completes": this.currentQuota.completes,
                    "isActive": this.currentQuota.selectedIsActive === "True",
                    "conditions": []
                };
                for(var i = 0; i < this.currentQuota.conditions.length; i++) {
                    quota.conditions.push(this.currentQuota.conditions[i].question);
                }
                project.targetAudiences[index].quotas.push(quota);
                this.LoadDefaultCurrentQuota();
            }
        },
        sortOrderforQuota(added, taId) 
        {
            if (added.item) {
                var project = useProjectStore().project;
                for (var i = 0; i < project.targetAudiences.length; i++)
                {
                    if(project.targetAudiences[i].tempId === taId)
                    {
                        var currQuota = project.targetAudiences[i].quotas;
                        var currIndex = added.oldIndex; var newIndex = added.newIndex;

                        var tmp = currQuota[currIndex];
                        currQuota.splice(currIndex, 1);
                        currQuota.splice(newIndex, 0, tmp);

                        for(var j = 0; j < currQuota.length; j++)
                        {
                            currQuota[j].order = j+1;
                        }

                        project.targetAudiences[i].quotas = currQuota;
                        break;
                    }
                }
            }
        },
        RemoveQuota(taId, qtId) {
            var project = useProjectStore().project;
            var taIndex = project.targetAudiences.findIndex(x => x.tempId === taId)
            if(taIndex > -1) {
                // var subInd = project.targetAudiences[taIndex].quotas.findIndex(x => x.tempId === qtId);
                // if(subInd > -1) {
                //     project.targetAudiences[taIndex].quotas.splice(subInd, 1);
                // }
                var quotaIndex = project.targetAudiences[taIndex].quotas.findIndex(x => x.tempId === qtId)
                if(quotaIndex > -1)
                project.targetAudiences[taIndex].quotas.splice(quotaIndex,1);

                for(var j = 0; j < project.targetAudiences[taIndex].quotas.length; j++)
                {
                    project.targetAudiences[taIndex].quotas[j].order = j+1;
                }
            }
        }
    }
})