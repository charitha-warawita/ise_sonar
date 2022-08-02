<script setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';

const emits = defineEmits(['creating']);
</script>

<template>
	<div class="target-audience-empty-container shadowed">
		<div class="centered-v">
			<p style="font-weight: bold">There are No Target Audiences</p>
			<p>Please create at least on target group to create the project</p>

			<div style="max-width: 250px; margin: 20px auto">
				<PrimaryButton
					square
					label="Create Target Audience"
					@click="emits('creating')"
				/>
			</div>
		</div>
	</div>
</template>

<style scoped>
.target-audience-empty-container {
	padding: 20px;
	text-align: center;
}
</style>
