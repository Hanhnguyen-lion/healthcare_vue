<template>
	<div class="modal-overlay">
		<div class="modal-content">
			<div class="modal-header">
				<h4 class="modal-title">Header</h4>
				<button type="button" class="btn-close" aria-label="Close" @click="handleClose"></button>
			</div>
			<div class="modal-body">
				<div class="container">
					<div class="form-group">
						<p>Initial Data: {{ input_data.name }}</p>
						<label for="title">Title</label>
						<input type="text" v-model="dataObj.title" class="form-control"/>
					</div>
					<div class="form-group">
						<label for="body">Body</label>
						<textarea rows="3" id="body" v-model="dataObj.body" class="form-control"/>
					</div>
				</div>
			</div>
			<div class="modal-footer">
				<button class="btn btn-outline-primary" @click="handleSave">Save</button>
				<button class="btn btn-outline-secondary" @click="handleClose">Close</button>
			</div>
		</div>
	</div>
</template>

<style scoped>
  .modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
  }
  .modal-content {
    background-color: white;
    padding: 20px;
    border-radius: 8px;
  }
  </style>

<script>

export default{
	name: "ModalComponent",
	props:["input_data"],
	data(){
		return {
			dataObj:{
				title:"",
				body:"",
				saveData:false
			}
		}
	},
	methods:{
		handleSave(){
			this.dataObj.saveData = true;
			this.handleClose();
		},
		handleClose(){
			this.$emit("close", this.dataObj);
		}
	},
	mounted(){
		console.log("data:", this.input_data);
	}
}

</script>