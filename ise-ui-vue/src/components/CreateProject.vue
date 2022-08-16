<template lang="">
    <div class="container">
        <div class="row">
            <div class="col-8">
                <div class="accordion" id="accordionPanelsStayOpenExample">
                    <div class="accordion-item customItem">
                        <h2 class="accordion-header" id="panelsStayOpen-headingZero">
                        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseZero" aria-expanded="true" aria-controls="panelsStayOpen-collapseZero">
                            <b>Project Settings</b>
                        </button>
                        </h2>
                        <div id="panelsStayOpen-collapseZero" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingZero">
                            <div class="accordion-body">
                                <div v-if="project.errors.length > 0" style="color:red">
                                Validation errors:<br/>
                                <span v-for="error in project.errors" :key="error" style="color:red">
                                    {{ error }}<br/>
                                </span></div>
                                <ProjectSetting />
                            </div>
                        </div>
                    </div>
                    <div v-if="project.targetAudiences" v-for="ta in project.targetAudiences" :key="ta.tempId">
                        <div class="accordion-item customItem">
                            <h2 class="accordion-header" id="panelsStayOpen-headingTwo">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" :data-bs-target="'#panelsStayOpen-collapseTwo-' + ta.tempId" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwo">
                                <b>Target Audience - {{ta.tempId}}</b>
                            </button>
                            </h2>
                            <div :id="'panelsStayOpen-collapseTwo-' + ta.tempId" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingTwo">
                                <div class="accordion-body">
                                    <div v-if="ta.errors.length > 0" style="color:red">
                                        Validation errors:<br/>
                                        <span v-for="error in ta.errors" :key="error" style="color:red">
                                            {{ error }}<br/>
                                        </span></div>
                                    <div class="row g-3">
                                        <div class="col-md-12">
                                            Complete this information to provide target audience details of your survey
                                        </div>
                                        <div class="col-md-12">
                                            <label for="inputEmail4" class="form-label">Name</label>
                                            <input type="text" class="form-control" id="inputEmail4" v-model="ta.name">
                                        </div>
                                        <div class="col-md-6">
                                            <label for="inputEmail4" class="form-label">Target Audience Order</label>
                                            <input type="number" class="form-control" id="inputEmail4" v-model="ta.audienceNumber">
                                        </div>
                                        <div class="col-md-6">
                                            <label for="inputEmail4" class="form-label">Estimated IR (in %)</label>
                                            <input type="number" class="form-control" id="inputEmail4" v-model="ta.estimatedIR" v-on:input="useProjStore.CalculateCharges($event)">
                                        </div>
                                        <div class="col-md-6">
                                            <label for="inputEmail4" class="form-label">Estimated LOI (in mins)</label>
                                            <input type="number" class="form-control" id="inputEmail4" v-model="ta.estimatedLOI" v-on:input="useProjStore.CalculateCharges($event)">
                                        </div>
                                        <div class="col-md-6">
                                            <label for="inputEmail4" class="form-label">Wanted Completes Count</label>
                                            <input type="number" class="form-control" id="inputEmail4" v-model="ta.limit" v-on:input="useProjStore.CalculateCharges($event)">
                                        </div>
                                        <div class="col-md-12">
                                            <label for="inputEmail4" class="form-label">Testing URL</label>
                                            <input type="text" class="form-control" id="inputEmail4" v-model="ta.testingUrl">
                                        </div>
                                        <div class="col-md-12">
                                            <label for="inputEmail4" class="form-label">Live URL</label>
                                            <input type="text" class="form-control" id="inputEmail4" v-model="ta.liveUrl">
                                        </div>
                                        <div class="accordion-item-custom">
                                            <h2 class="accordion-header" id="panelsStayOpen-headingThree">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" :data-bs-target="'#panelsStayOpen-collapseThree-' + ta.tempId" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwo">
                                                    <b>Qualification</b>
                                                </button>
                                            </h2>
                                            <div :id="'panelsStayOpen-collapseThree-' + ta.tempId" class="accordion-collapse collapse" aria-labelledby="panelsStayOpen-headingThree">
                                                <div class="accordion-body">
                                                    <draggable  v-on:update="useQualStore.sortOrderforQual($event, ta.tempId)" >
                                                        <div class="subDivQ row g-3" v-if="ta.qualifications" v-for="(qual, index) in ta.qualifications" :key="qual.tempId">
                                                            <div class="col-md-1">{{qual.order}}</div>
                                                            <div class="col-md-8">
                                                                <div class="col-md-12"><b>{{qual.question.categoryName}} - {{qual.question.name}}</b></div>
                                                                <div class="col-md-12">{{qual.question.text}}</div>
                                                                <div class="col-md-12">
                                                                    <div style="display: inline-block"  v-for="(item) in qual.question.variables" :key="item.id">
                                                                        <div class="form-check">
                                                                            <label style="background-color: lightgrey; border-radius:5px; padding: 0 10px 0 10px"><i>{{item.name}}</i></label>
                                                                        </div>
                                                                    </div>
                                                                </div>       
                                                            </div>
                                                            <div class="col-md-3">
                                                                <button 
                                                                    type="button" 
                                                                    @click="useQualStore.UpdateQualLogOperation(ta.tempId, qual.tempId, 'AND')"
                                                                    class="btn btn-outline-success QualLogicalButton"
                                                                    :class="[qual.logicalDecision !== 'OR' ? 'searchButton' : 'btn-light']"
                                                                    >Must</button>
                                                                <button 
                                                                    type="button" 
                                                                    @click="useQualStore.UpdateQualLogOperation(ta.tempId, qual.tempId, 'OR')"
                                                                    class="btn btn-outline-success QualLogicalButton"
                                                                    :class="[qual.logicalDecision === 'OR' ? 'searchButton' : 'btn-light']"
                                                                    >Optional</button>
                                                                <a @click="useQualStore.RemoveQualification(ta.tempId, qual.tempId)" class="link-danger" style="float:right; margin-top:10%">
                                                                Remove</a>
                                                            </div>
                                                            <hr/>
                                                        </div>
                                                    </draggable>    
                                                <div class="col-md-12">
                                                    <button class="btn btn-outline-success searchButton mb-4 "  style="float: right" @click="toggleModal('qual'+ ta.tempId);useQualStore.GetProfileCategories(ta.tempId)">Add Qualification</button>
                                                    <CustomModal @close="toggleModal('qual'+ ta.tempId)" :modalId="'qual'+ ta.tempId">
                                                        <div class="card modal-content">
                                                            <h3 class="card-header">Qualifications</h3>
                                                            <div class="card-body">
                                                                <QualificationsList @close="toggleModal('qual'+ ta.tempId)" itemType='profileVars' :taId='ta.tempId' :qualificationId="'qual-' + ta.tempId" />
                                                            </div>
                                                        </div>
                                                    </CustomModal>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="accordion-item-custom">
                                            <h2 class="accordion-header" id="panelsStayOpen-headingFour">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" :data-bs-target="'#panelsStayOpen-collapseFour-' + ta.tempId" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwo">
                                                    <b>Quota</b>
                                                </button>
                                            </h2>
                                        <div :id="'panelsStayOpen-collapseFour-' + ta.tempId" class="accordion-collapse collapse" aria-labelledby="panelsStayOpen-headingFour">
                                            <div class="accordion-body">
                                                <div class="container">
                                                    <div class="subDivQ row g-3" v-if="ta.quotas" v-for="(qt, index) in ta.quotas" :key="qt.tempId">
                                                        <div class="col-md-1">{{qt.tempId }}</div>
                                                        <div class="col-md-9">
                                                            <div class="col-md-12"><b>{{qt.quotaName}}</b></div>
                                                            <div class="col-md-12">Field Target: {{qt.fieldTarget}}; Limit: {{ qt.limit }}</div>
                                                            <div v-for="condition in qt.conditions" :key="condition.id">
                                                                <div class="col-md-12">{{condition.categoryName}} - {{condition.name}}</div>
                                                                <div class="col-md-12">
                                                                    <div style="display: inline-block"  v-for="(item) in condition.variables" :key="item.id">
                                                                        <div class="form-check">
                                                                            <label style="background-color: lightgrey; border-radius:5px; padding: 0 10px 0 10px"><i>{{item.name}}</i></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>       
                                                        </div>
                                                        <div class="col-md-2">
                                                            <a @click="useQuotaDataStore.RemoveQuota(ta.tempId, qt.tempId)" class="link-danger" style="float:right; margin-top:10%">
                                                            Remove</a>
                                                        </div>
                                                        <hr/>
                                                    </div>
                                                    <button class="btn btn-outline-success searchButton mb-4" id="addQutobutton" @click="toggleModal('quota'+ ta.tempId); useQuotaDataStore.LoadDefaultCurrentQuota();">Add Quota</button>  
                                                    <CustomModal @close="toggleModal('quota'+ ta.tempId)" :modalId="'quota'+ ta.tempId">
                                                        <div class="card modal-content">
                                                            <h3 class="card-header">Quota</h3>
                                                            <div class="card-body">
                                                                <QuotaList :taId=ta.tempId :totalCompletes=ta.limit />
                                                            </div>               
                                                        </div> 
                                                    </CustomModal>                                                  
                                                </div>
                                            </div>
                                        </div>      
                                        <div class="col-md-12">
                                            <button :class="{ hidden: ta.tempId === 1 }" style="float: right" class="btn btn-outline-success btn-light me-2 " v-on:click="useProjStore.CancelTargetAudience(ta)">Cancel Target Audience</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <button class="btn btn-outline-success btn-light me-2" style="margin-bottom: 20px;" v-on:click="useProjStore.AddTargetAudienceElement()">Add another Target Audience</button>
                </div>
            </div>
        </div>     
        <div class="col-4">
            <div class="costSection">
                <div class="row">
                    <div class="col-md-12">
                    <label class="btn costLabel"><b>Cost Estimation</b></label>
                    </div><div class="breakDiv"></div>
                    <div class="row" v-if="project.targetAudiences" v-for="ta in project.targetAudiences" :key="ta.tempId">
                        <h5>Estimation of Target audience - {{ta.tempId}} </h5><div class="breakDiv"></div>
                        <div class="col-md-8">
                            <label for="inputEmail4" class="form-label">CPI</label>
                        </div>
                        <div class="col-md-4">
                            <label for="inputEmail4" class="form-label">{{ta.costPerInterview}} USD</label>
                        </div>
                        <div class="col-md-8">
                            <label for="inputEmail4" class="form-label">CPTG</label>
                        </div>
                        <div class="col-md-4">
                            <label for="inputEmail4" class="form-label">{{ta.cptg}} USD</label>
                        </div><div class="col-md-12"></div>
                        <div class="col-md-8">
                            <label for="inputEmail4" class="form-label"><b>Subtotal</b></label>
                        </div>
                        <div class="col-md-4">
                            <label for="inputEmail4" class="form-label"><b>{{ta.subtotal}} USD</b></label>
                        </div><div class="breakDiv"></div><hr/><div class="breakDiv"></div>
                    </div>
                    
                    <div class="col-md-12">
                        <label for="inputEmail4" class="form-label" style="margin: auto"><h4>Total: {{totalCost}} USD</h4></label>
                    </div>
                    <div class="breakDiv"></div><hr>
                    <div class="col-md-12">
                        <button @click="SubmitProject(project)" class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;">Create Project</button>
                        <button @click="SaveforLater(project)" class="btn btn-outline-success btn-light me-2" style="width:100%; margin: 5px 0;">Save as Draft</button>
                        <button @click="DiscardProject()" class="btn btn-outline-success btn-light me-2" style="width:100%; margin: 5px 0;">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</template>
<script setup>
import PageTitle from '@/components/PageTitle.vue'
import CustomModal from '@/components/CustomModal.vue'
import QualificationsList from '@/components/QualificationsList.vue'
import QuotaList from '@/components/QuotaList.vue'
import ProjectSetting from '@/components/ProjectSetting.vue'
import {useProjectStore} from '@/stores/projectStore'
import {useQualificationStore} from '@/stores/qualificationStore'
import {useQuotaStore} from '@/stores/quotaStore'
import {storeToRefs} from 'pinia'
import { onMounted } from 'vue'
import {ref} from "vue"
import { VueDraggableNext } from "vue-draggable-next";
import { useRouter } from 'vue-router';

import useVuelidate from '@vuelidate/core'
import { helpers, required, minLength, email, minValue } from '@vuelidate/validators'

defineProps(['open'])
var draggable = VueDraggableNext
var useProjStore = useProjectStore()
var useQualStore = useQualificationStore()
var useQuotaDataStore = useQuotaStore()
const { project, basicSettingDesc, totalCost, saveProjectLoading, saveProjectError } = storeToRefs(useProjStore)

const rules = {
    name: { required },
    reference: { required },
    startDate: { required },
    fieldingPeriod: { required, minValueValue:minValue(1) },
    // testingUrl: { required },
    // liveUrl: { required },
    categories: { required },
    user: { "name": { required }, "email": { required, email }  },
    targetAudiences: {
        $each: helpers.forEach({
            "name": { required: helpers.withMessage('TA Name cannot be empty', required) },
            "audienceNumber":{ required: helpers.withMessage('Audience order cannot be empty', required) },
            "estimatedIR": { required, minValueValue: helpers.withMessage(() => "Estimated IR minimum value allowed is 1", minValue(1))   },
            "estimatedLOI": { required, minValueValue: helpers.withMessage(() => 'Estimate LOI minimum value allowed is 1', minValue(1)) },
            "limit": { required, minValueValue:helpers.withMessage(() => 'wanted Completes minimum value allowed is 1', minValue(1)) },
            "testingUrl": { required:helpers.withMessage(() => 'Testing survey URL under target Audiences cannot be empty', required) },
            "liveUrl": { required:helpers.withMessage(() => 'Live survey URL under target Audiences cannot be empty', required) }
        })
    }
};

const saveForLaterRules = {
    name: { required },
    reference: { required },
    startDate: { required },
    categories: { required },
    user: { "name": { required }, "email": { required, email }  }
};
const router = useRouter();

async function SubmitProject(project) {
    project.lastUpdate = new Date();
    if(await ProjectValidated(project, 'create')) {
        if(await useProjStore.ProjectAPIValidated(project))
            router.push('/confirm');
    }
}
async function SaveforLater(project) {
    if(await ProjectValidated(project, 'save')) {
        await useProjStore.CreateProject(project);
        if(typeof(project.tempId) === undefined || project.errors.length > 0) {
            var newLine = "\r\n"
            var alertMessage = 'Project Save as Draft is unsuccessful. Project ID returned is: ' + this.project.tempId + '.' + newLine;
            alertMessage += 'Error returned are: ' + newLine;
            for(var i =0; i < this.project.errors.length; i++)
                alertMessage += this.project.errors[i] + newLine;
            alert(alertMessage);
        }
        else {
            alert('project saved as draft successfully. New project ID is ' + project.tempId);
            useProjStore.$reset();
            router.push('/');
        }
        
    }
}
function DiscardProject() {
    if(confirm("Are you sure you want to cancel? All your changes will be discarded?")) {
        useProjStore.$reset();
        router.push('/');
    }
}
async function ProjectValidated(project, action) {
    var v$ = {};
    if(action === 'create')
        v$ = useVuelidate(rules, project);
    else 
        v$ = useVuelidate(saveForLaterRules, project);

    const result = await v$.value.$validate();
    project.errors = [];
    for(var i = 0; i < project.targetAudiences.length; i++)
        project.targetAudiences[i].errors = [];

    if(!result) {
        for(var i = 0; i< v$.value.$errors.length; i++) {
            if(Array.isArray(v$.value.$errors[i].$message)) {
                for(var j=0; j < v$.value.$errors[i].$message.length; j++)
                {
                    for(var k=0; k < v$.value.$errors[i].$message[j].length; k++)
                    {
                        if(v$.value.$errors[i].$propertyPath === 'targetAudiences')
                            project.targetAudiences[j].errors.push(v$.value.$errors[i].$propertyPath + (j+1) + ' - ' +  v$.value.$errors[i].$message[j][k]);
                    }
                }
            }
            else {
                project.errors.push(v$.value.$errors[i].$propertyPath + ' - ' + v$.value.$errors[i].$message);
            }
        }
    }

    return result;
}

const toggleModal = (id) => {
    // useQuotaDataStore.conditiongrid = false;
    console.log('customModal id or qId is:' + id);
    var classname = 'customModal-' + id
    var element = document.getElementsByClassName(classname)
    if(element[0].style.display === 'none') {
        element[0].style.removeProperty("display")
        element[1].style.removeProperty("display")
    }
    else { 
        element[0].style.display = 'none'
        element[1].style.display = 'none'
    }
};

onMounted(() => {
    useProjStore.$reset()
    useQualStore.$reset()
    useQuotaDataStore.$reset()
    useProjStore.AddTargetAudienceElement()
})
</script>
<style>
.costSection {
    box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px; 
    padding: 15px;
}
.costLabel {
    width:100%; 
    border-color: lightgray; 
    color: #0c63e4; 
    background-color: #e7f1ff; 
}
.hidden {
    display: none;
}
.QualLogicalButton {
    font-size:0.80em;
    min-width:72px;
}
.subDiv {
    margin: 20px auto;
    box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
    padding-bottom: 20px;
    background-color: #E8E8E8;
}

.subDivQ {
    margin: 10px auto;
    background-color: white;
    /*padding-bottom: 20px;*/
}

.customItem {
    margin-bottom: 10px;
    box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
}

input[type=checkbox] {
    transform: scale(1.5);
}

.breakDiv {
    margin:10px 0;
}

.accordion-item-custom {
    box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
    padding:0;
}

  .modal-content {
    display: flex;
    flex-direction: column;
  }

  .modal-content h1, p {
      margin-bottom: 16px;
    }
  .modal-content h1 {
      font-size: 32px;
    }
  .modal-content p {
      font-size: 18px;
    }

    .my-custom-scrollbar {
position: relative;
height: 200px;
overflow: auto;
}
.table-wrapper-scroll-y {
display: block;
}
td a { 
   display: block; 
    color: black;
}

#addQutobutton{
    float: right;
}
    
</style>