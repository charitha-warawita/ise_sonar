<script setup lang="ts"></script>

<template>
	<div class="card">
		<div class="card-header">
			<slot name="header"></slot>
		</div>

		<div class="card-body">
			<slot name="body"></slot>
		</div>
	</div>
</template>

<style scoped lang="scss"></style>
