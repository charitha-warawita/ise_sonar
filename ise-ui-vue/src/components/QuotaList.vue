<template>
  <div v-show="!useQuotaDataStore.conditiongrid">
    <fieldset class="col-md-12 border p-2 fieldsetstyle">
      <legend class="float-none w-auto" style="font-size: 18px">
        Create - Section 1 of 2
      </legend>
      <div class="row g-2">
        <div v-if="useQuotaDataStore.currentQuota.errors && useQuotaDataStore.currentQuota.errors.length > 0" style="color:red">
          Validation errors:<br/>
          <span v-for="error in useQuotaDataStore.currentQuota.errors" :key="error" style="color:red">
              {{ error }}<br/>
          </span>
        </div>
        <div class="col-md-12">
          <label for="input" class="form-label">Name</label>
          <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.name" />
        </div>
        <div class="col-md-9">
          <label for="selSurveyQuotaType" class="form-label">Survey Quota Type</label>
          <select @change="useQuotaDataStore.serveyQuotaTyp($event)" class="form-select" id="selSurveyQuotaType" name="sellist1"
                ref="surveyQuotaType"> 
            <option v-bind:value="ServeyQuotaTypes" v-for="ServeyQuotaTypes in useQuotaDataStore.currentQuota.ServeyQuotaType" 
                :key="ServeyQuotaTypes" :disabled="ServeyQuotaTypes == 'Started'" :selected="ServeyQuotaTypes == 'Completed'" >
              {{ ServeyQuotaTypes }}
            </option>
            <!-- <label for="sel1" class="form-label">Select list (select one):</label> -->
          </select>
        </div>
        <div class="col-md-1"></div>
        <div class="col-md-2">
          <label for="input" class="form-label">Completes</label>
          <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.completes" />
        </div>
        <div class="col-md-9">
          <label for="selAdjustmentType" class="form-label">Adjustment Type</label>
          <select @change="useQuotaDataStore.adjustmentType($event)" class="form-select" id="selAdjustmentType" name="sellist2">
            <option v-bind:value="adjustmentType" v-for="adjustmentType in useQuotaDataStore.currentQuota.adjustmentType" :key="adjustmentType">
              {{ adjustmentType }}
            </option>
          </select>
        </div>
        <div class="col-md-1"></div>
        <div class="col-md-2">
          <label for="input" class="form-label">Prescreens</label>
          <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.prescreens" />
        </div>
        <div class="col-md-9">
          <label for="input" class="form-label">Is Active</label>
          <select @change="useQuotaDataStore.isActiveState($event)" class="form-select" id="sel3" name="sellist3">
            <option v-bind:value="isAct" v-for="isAct in useQuotaDataStore.currentQuota.isActive" :key="isAct">{{ isAct }}</option>
          </select>
          <!--<input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.isActive"/>-->
        </div>
        <div class="col-md-6">
          <div class="row">
            <div class="col col-lg-4" style="margin-top: 8%">
              <label for="input" class="form-label">Field Target</label>
            </div>
            <div class="col col-lg-3">
              <label for="input" class="form-label">Nominal</label>
              <input :readonly="useQuotaDataStore.currentQuota.selectedAdjustmentType !== 'nominal'" 
              type="text" class="form-control" @change="useQuotaDataStore.UpdateAdjValue(totalCompletes, 'fieldTarget', 'nominal')"
                v-model="useQuotaDataStore.currentQuota.fieldTargetNominal"/>
            </div>
            <div class="col col-lg-3">
              <label for="input" class="form-label">Percentage(%)</label>
              <input :readonly="useQuotaDataStore.currentQuota.selectedAdjustmentType !== 'percentage'" type="text" class="form-control"
                @change="useQuotaDataStore.UpdateAdjValue(totalCompletes, 'fieldTarget', 'percentage')"
                v-model="useQuotaDataStore.currentQuota.fieldTargetPercentage" />
            </div>
          </div>
        </div>
        <div class="col-md-6">
          <div class="row">
            <div class="col col-lg-4" style="margin-top: 8%">
              <label for="input" class="form-label">Quota</label>
            </div>
            <div class="col col-lg-3">
              <label for="input" class="form-label">Nominal</label>
              <input :readonly="useQuotaDataStore.currentQuota.selectedAdjustmentType !== 'nominal'" type="text"
                class="form-control" @change="useQuotaDataStore.UpdateAdjValue(totalCompletes, 'quota', 'nominal')"
                v-model="useQuotaDataStore.currentQuota.quotaNominal"/>
            </div>
            <div class="col col-lg-3">
              <label for="input" class="form-label">Percentage(%)</label>
              <input :readonly="useQuotaDataStore.currentQuota.selectedAdjustmentType !== 'percentage'" type="text"
                class="form-control" @change="useQuotaDataStore.UpdateAdjValue(totalCompletes, 'quota', 'percentage')"
                v-model="useQuotaDataStore.currentQuota.quotaPercentage"/>
            </div>
          </div>
        </div>
      </div>
      <div>
        <table v-show="useQuotaDataStore.enableSave" class="table table-striped" style="margin-top: 1%">
          <thead>
              <tr class="tableHeader">
              <th><b>Name</b></th>
              <th><b>Variables</b></th>
              <th><b>Action</b></th>
              </tr>
          </thead>
          <tbody>
              <tr v-for="condition in useQuotaDataStore.currentQuota.conditions" :key="condition.tempId">
              <th>{{condition.question.name}}</th>
              <td><label v-for="variable in condition.question.variables" :key="variable.id"> | {{variable.name}} |</label></td>
              <td><a @click="useQuotaDataStore.RemoveCondition(condition.tempId)" class="link-danger" style="float:right; margin-top:10%; cursor:pointer;">Remove</a></td>
              </tr>
          </tbody>
        </table>
      </div>
      <div class="col-md-12 p-4">
        <button class="btn btn-outline-success searchButton" id="addQutobutton" v-on:click="useQuotaDataStore.addCondition(taId)">
          Add condition
        </button>
      </div>
    </fieldset>
  </div>
  <div v-show="useQuotaDataStore.conditiongrid">
    <fieldset class="col-md-12 border p-2 fieldsetstyle">
      <legend class="float-none w-auto" style="font-size: 18px">
        Condition - Section 2 of 2
      </legend>
      <div class="container">
        <div class="row">
          <div class="col-md-3">
            <label for="selCondition" class="form-label">Select Condition</label>
          </div>
          <div class="col-md-6">
            <select class="form-select" id="selCondition" name="sellist1" @change="useQuotaDataStore.selectQuotaCondition($event)">
              <option disables selected value>--Select--</option>
              <option v-bind:value="condition.order" v-for="condition in useQuotaDataStore.conditionlist" :key="condition.tempId">
                {{ condition.order }}-{{ condition.question.categoryName }}-{{ condition.question.name }}
              </option>
            </select>
          </div>
        </div>
      </div>
      <div
        class="card"
        style="margin-top: 5%; border: 1px solid #8f959b !important"
        v-show="useQuotaDataStore.showConditionDetail"
      >
        <div div class="card-body">
          <div class="subDivQ row g-3">
            <div v-if="useQuotaDataStore.currentCondition" class="col-md-9">
              <label style="display: none">{{
                useQuotaDataStore.currentCondition.id
              }}</label>
              <div class="col-md-12">
                <b>{{ useQuotaDataStore.currentCondition.question.categoryName }} {{ useQuotaDataStore.currentCondition.question.name }}</b>
              </div>
              <div class="col-md-12">
                {{ useQuotaDataStore.currentCondition.question.text }}
              </div>
              <div class="col-md-12">
                <div style="display: inline-block">
                  <div class="form-check">
                    <button v-for="answer in useQuotaDataStore.currentCondition.question.variables" :key="answer.id" type="button"
                      :id="'answer' + answer.id" @click="useQuotaDataStore.SelectQuotaConditionAnswer(answer.id)"
                      class="btn btn-outline-success me-2 projSettingTogButton" :class="[answer.selected ? 'searchButton' : 'btn-light']">
                      {{ answer.name }}
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <button class="btn btn-outline-success btn-light me-2" style="float:left; margin: 5px 0" v-on:click="useQuotaDataStore.GoToFirstSection()">
      back
      </button>
      <button class="btn btn-outline-success searchButton me-2" style="float:right; margin: 5px 0" v-on:click="useQuotaDataStore.SaveCondition()">
      Save Condition
      </button>
    </fieldset>
  </div>
  <div v-show="useQuotaDataStore.enableSave" class="col-md-12 p-4">
    <button
      class="btn btn-outline-success searchButton me-2"
      style="width: 100%; margin: 5px 0"
      v-on:click="SaveCurrentQuota(taId)"
    >
      Save quota
    </button>
  </div>
</template>
<script setup>
import { useQuotaStore } from "@/stores/quotaStore";
import { storeToRefs } from "pinia";
import { onMounted } from "vue";

import useVuelidate from '@vuelidate/core'
import { helpers, required, minLength, email, minValue, url, requiredIf } from '@vuelidate/validators'

const props = defineProps(["taId", "quotaid", "totalCompletes"]);

var useQuotaDataStore = useQuotaStore();

const {
  name,
  fieldTarget,
  status,
  completes,
  prescreence,
} = storeToRefs(useQuotaDataStore);

// const rules = {
//     name: { required },
//     ServeyQuotaType: { required : requiredIf(function () {return useQuotaDataStore.currentQuota.selectedServeyQuotaType=='--select--'})},    
//     adjustmentType: { required : requiredIf(function () {return useQuotaDataStore.currentQuota.selectedAdjustmentType =='--select--'})},
//     fieldTargetNominal: { required : requiredIf(function () {return useQuotaDataStore.currentQuota.selectedAdjustmentType =='Nominal'}), minValueValue: helpers.withMessage(() => "Field Target Nominal minimum value allowed is 1", minValue(1))},
//     fieldTargetPercentage: { required  : requiredIf(function () {return useQuotaDataStore.currentQuota.selectedAdjustmentType =='percentage'}), minValueValue: helpers.withMessage(() => "Field Target Percentage minimum value allowed is 1", minValue(1)) },
//     quotaNominal: { required  : requiredIf(function () {return useQuotaDataStore.currentQuota.selectedAdjustmentType =='Nominal'}), minValueValue: helpers.withMessage(() => "Quota Nominal minimum value allowed is 1", minValue(1)) },
//     quotaPercentage: { required  :  requiredIf(function () {return useQuotaDataStore.currentQuota.selectedAdjustmentType =='percentage'}), minValueValue: helpers.withMessage(() => "Quota Percentage minimum value allowed is 1", minValue(1)) },
// };

const rules = {
    name: { required},
    selectedServeyQuotaType: { required : requiredIf(function () {
        return (this.selectedServeyQuotaType !='');
    })},    
    selectedAdjustmentType: { required},
    fieldTargetNominal: { required, minValueValue:minValue(1)},
    fieldTargetPercentage: { required, minValueValue:minValue(1)},
    quotaNominal: { required, minValueValue:minValue(1) },
    quotaPercentage: { required, minValueValue:minValue(1)},
};

async function SaveCurrentQuota(taId) {
   //if(await QuotaValidated(taId)) {
      useQuotaDataStore.SaveQuota(taId);
      document.getElementById("selSurveyQuotaType").selectedIndex = 2;
      document.getElementById("selAdjustmentType").selectedIndex = 0;
      document.getElementById("selCondition").selectedIndex = 0;
   //}
}

onMounted(() => {
  useQuotaDataStore.$reset()
})

async function QuotaValidated(taId) {
    var v$ = {};
    v$ = useVuelidate(rules, useQuotaDataStore.currentQuota);

    const result = await v$.value.$validate();
    useQuotaDataStore.currentQuota.errors = [];

    if(!result) {
        for(var i = 0; i< v$.value.$errors.length; i++) {
          useQuotaDataStore.currentQuota.errors.push(Capitalising(v$.value.$errors[i].$propertyPath) + ' - ' + v$.value.$errors[i].$message);
        }
    }

    return result;
}

function Capitalising(data) {
    var capitalized = []
    data.split(' ').forEach(word => {
        capitalized.push(
        word.charAt(0).toUpperCase() +
        word.slice(1).toLowerCase()
        )
    })
    return capitalized.join(' ');
}
</script>

<style>
.fieldsetstyle {
  border: 1px solid #8f959b !important;
}
.quotaModel {
  max-height: 600px;
  min-height: 350px;
  overflow-y: auto;
}
</style>
