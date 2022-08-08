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
        currentQuota: {}
    }),
    actions: {
        LoadDefaultCurrentQuota() {
            this.currentQuota = {
                name: '',
                selectedServeyQuotaType:'',
                selectedAdjustmentType:'',
                fieldTargetPercentage: 0,
                fieldTargetNominal: 0,
                status: '',
                completes: 0,
                prescreens: 0,
                quotaPercentage: 0,
                quotaNominal: 0,
                conditions:[],
                ServeyQuotaType:["--Select--","Started","Completed"],
                adjustmentType:["--Select--", "Nominal","percentage"],
            };
            this.enableSave = false;
            this.conditionlist = [];
            this.currentCondition = null;
            this.showConditionDetail = false;

        },
        serveyQuotaTyp(event){
            this.currentQuota.selectedServeyQuotaType = event.target.value.toLowerCase();    
        },
        adjustmentType(event){
            var selectedValue = event.target.value.toLowerCase();  
            this.currentQuota.selectedAdjustmentType = selectedValue
        },
        UpdateAdjValue(totalCompletes, element, key) {
            if(Number.isInteger(totalCompletes)) {
                if(element === 'fieldTarget') {
                    if(key === 'nominal')
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
        addCondition(){
            this.conditionlist = [];
            this.showConditionDetail =false;
            this.conditiongrid = true;
            var project = useProjectStore().project;
            for (var i = 0; i < project.projectTargetAudiences.length; i++)
            {
                for(var j = 0; j < project.projectTargetAudiences[i].qualifications.length; j++)
                {
                    this.conditionlist.push(JSON.parse(JSON.stringify(project.projectTargetAudiences[i].qualifications[j])));
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
            var index = this.currentQuota.conditions.findIndex(x => x.id === conditionId);
            if(index > -1)
                this.currentQuota.conditions.splice(index, 1);

            if(this.currentQuota,conditions.length < 0)
                this.enableSave = false;
        },
        SaveQuota(taid){
           
            var project = useProjectStore().project;
            var index = project.projectTargetAudiences.findIndex(x => x.id === taid)
            var quotaId = project.projectTargetAudiences[index].quota.length + 1;
            if(index > -1) {
                var quota = {
                    "id": quotaId,
                    "quotaName": this.currentQuota.name,
                    "quotaType": this.currentQuota.selectedServeyQuotaType,
                    "fieldTarget": this.currentQuota.fieldTargetNominal,
                    "limit": this.currentQuota.quotaNominal,
                    "prescreens": this.currentQuota.prescreens,
                    "completes": this.currentQuota.completes,
                    "isActive": this.currentQuota.status,
                    "conditions": this.currentQuota.conditions
                };
                project.projectTargetAudiences[index].quota.push(quota);
                this.LoadDefaultCurrentQuota();
            }
        },
        RemoveQuota(taId, qtId) {
            var project = useProjectStore().project;
            var index = project.projectTargetAudiences.findIndex(x => x.id === taId)
            if(index > -1) {
                var subInd = project.projectTargetAudiences[index].quota.findIndex(x => x.id === qtId);
                if(subInd > -1) {
                    project.projectTargetAudiences[i].quota.splice(subInd, 1);
                }
            }
        }
    }
})