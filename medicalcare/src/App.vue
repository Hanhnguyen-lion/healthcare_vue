<template>
    <header>
      <nav class="navbar navbar-expand-lg" style="background-color:rgb(35, 64, 196, 0.6);" data-bs-theme="light">
        <div class="container-fluid">
          <div class="navbar-collapse collapse d-sm-inline-flex">
            <div class="text-white">
                <h1><b>HEALTH CARE</b></h1>
                <h6> Hospital Management System</h6>
            </div>
          </div>
          <div class="navbar-collapse collapse">
            <ul class="navbar-nav flex-grow-1 justify-content-end">
              <li class="nav-item">
                <RouterLink to="/" class="nav-link text-white fw-bold">Home
                </RouterLink>
              </li>
              <li class="nav-item dropdown text-white">
                  <a class="nav-link dropdown-toggle text-white fw-bold" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                      Category
                  </a>
                  <ul class="dropdown-menu">
                      <li><RouterLink class="dropdown-item" to ="/Medicine/Category">Medicine Category</RouterLink></li>
                      <li><RouterLink class="dropdown-item" to ="/Treatement/Category">Treatment Category</RouterLink></li>
                  </ul>
              </li>
              <li class="nav-item">
                <RouterLink class="nav-link text-white fw-bold" to ="/Hospital">Hospital</RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink class="nav-link text-white fw-bold" to ="/Department">Department</RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink class="nav-link text-white fw-bold" to ="/Medicine">Medicine</RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink class="nav-link text-white fw-bold" to ="/Doctor">Doctor</RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink class="nav-link text-white fw-bold" to ="/Patient">Patient</RouterLink>
              </li>
              <li class="nav-item dropdown text-white">
                  <a class="nav-link dropdown-toggle text-white fw-bold" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                    Billing
                  </a>
                  <ul class="dropdown-menu">
                      <li><RouterLink class="dropdown-item" to ="/Billing/Add">Add Billing</RouterLink></li>
                      <li><RouterLink class="dropdown-item" to ="/Billing">Billing List</RouterLink></li>
                  </ul>
              </li>
              <li class="nav-item">
                <RouterLink class="nav-link text-white fw-bold" to ="/MedicalCare/Print">Medical Care</RouterLink>
              </li>
              <li class="nav-item">
                <RouterLink to="/Account/Login" class="nav-link text-white fw-bold">Logout</RouterLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </header>
    <vue3-confirm-dialog></vue3-confirm-dialog>
    <router-view></router-view>
  </template>
<script>
export default{
  methods: {
    logout(){
      this.$router.push("/Account/Login");
    }
  }
}
</script>
