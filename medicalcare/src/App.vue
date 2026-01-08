<template>
    <header>
      <nav class="navbar navbar-expand-lg" style="background-color:rgb(123, 140, 220); z-index: 1001;" data-bs-theme="light">
        <div class="container-fluid">
          <div class="navbar-collapse collapse">
            <RouterLink to="/" class="nav-link text-white">
              <div>
                  <h1><b>HEALTH CARE</b></h1>
                  <h6>Hospital Management System</h6>
              </div>
            </RouterLink>
          </div>
          <div class="navbar-collapse collapse">
            <ul class="navbar-nav flex-grow-1 justify-content-end">
              <li v-if="showMenu()" class="nav-item">
                <RouterLink to="/" class="nav-link text-white fw-bold">{{ $t('menu.home') }}
                </RouterLink>
              </li>
              <li v-if="showMenu()" class="nav-item">
                <a @click="logout()" class="nav-link text-white fw-bold" href="">{{ $t('menu.logout') }}</a>
              </li>
              <li class="nav-item nav-link text-white fw-bold">{{ $t('menu.language') }}:
                <select @change="switchLanguage" v-model="selected_language">
                  <option v-for="language in languages" :key="language.language" :value="language.language">
                    {{language.title}}
                  </option>
                </select>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </header>
    <div class="container-fluid">
      <div class="row">
        <!-- Sidebar/Vertical Menu Area -->
        <nav id="sidebarMenu" class="col-lg-2 d-md-block sidebar collapse">
          <div v-if="showMenu()" class="position-sticky pt-3">
            <ul class="nav flex-column">
                <li class="nav-item">
                  <a class="nav-link text-white fw-bold" data-bs-toggle="collapse" data-bs-target="#sub_menu_0" href="#">{{ $t('menu.category') }}<i class="bi small bi-caret-down-fill"></i> </a>
                  <ul id="sub_menu_0" class="submenu expand" data-bs-parent="#accordion">
                    <li><RouterLink class="nav-link text-white fw-bold" to ="/Medicine/Category">{{ $t('menu.medicine') }}</RouterLink></li>
                    <li><RouterLink class="nav-link text-white fw-bold" to ="/Treatement/Category">{{ $t('menu.treatment') }}</RouterLink></li>
                  </ul>
                </li>
                <li class="nav-item">
                  <RouterLink class="nav-link text-white fw-bold" to ="/Medicine">{{ $t('menu.medicine') }}</RouterLink>
                </li>
                <li class="nav-item">
                  <RouterLink class="nav-link text-white fw-bold" to ="/Appointment">{{ $t('menu.appointment') }}</RouterLink>
                </li>
                <li v-if="superAdmin()" class="nav-item">
                  <RouterLink class="nav-link text-white fw-bold" to ="/Hospital">{{ $t('menu.hospital') }}</RouterLink>
                </li>
                <li class="nav-item">
                  <RouterLink class="nav-link text-white fw-bold" to ="/Department">{{ $t('menu.department') }}</RouterLink>
                </li>
                <li class="nav-item">
                  <RouterLink class="nav-link text-white fw-bold" to ="/Doctor">{{ $t('menu.doctor') }}</RouterLink>
                </li>
                <li class="nav-item">
                  <RouterLink class="nav-link text-white fw-bold" to ="/Patient">{{ $t('menu.patient') }}</RouterLink>
                </li>
                <li class="nav-item">
                  <a class="nav-link text-white fw-bold" data-bs-toggle="collapse" data-bs-target="#sub_menu_1" href="#">{{ $t('menu.billing') }}<i class="bi small bi-caret-down-fill"></i> </a>
                  <ul id="sub_menu_1" class="submenu expand" data-bs-parent="#accordion">
                    <li><RouterLink class="nav-link text-white fw-bold" to ="/Billing/Add">{{ $t('menu.addBilling') }}</RouterLink></li>
                    <li><RouterLink class="nav-link text-white fw-bold" to ="/Billing">{{ $t('menu.billingList') }}</RouterLink></li>
                  </ul>
                </li>
                <li class="nav-item">
                  <RouterLink class="nav-link text-white fw-bold" to ="/MedicalCare/Print">{{ $t('menu.medicalCare') }}</RouterLink>
                </li>
            </ul>
          </div>
        </nav>

        <!-- Main Content Area -->
        <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
          <router-view></router-view>
        </main>
      </div>
    </div>    
    <vue3-confirm-dialog></vue3-confirm-dialog>
  </template>
<script>
import { isSupperAdmin } from './components/helper/helper';
import { useAuthStore } from './store/auth.module';
  export default{
    data(){
      return {
        auth : useAuthStore(),
        languages: [
              { flag: 'us', language: 'en', title: 'English' },
              { flag: 'vn', language: 'vn', title: 'Vietnamese'},
              { flag: 'ja', language: 'ja', title: 'Japanese' }
        ],
        selected_language:"en"
      }
    },
    methods:{
      logout(){
        this.auth.logout();
      },
      showMenu(){
        return (this.auth.accountLogin) ? true : false;
      },
      superAdmin(){
        return isSupperAdmin(this.auth.accountLogin);
      },
      switchLanguage(e){
        var locale = e.target.value;
        this.$i18n.locale = locale;
        sessionStorage.setItem("language_selected", locale);
      },
      getSelectedLanguage(){
        if (sessionStorage.getItem("language_selected"))
          return sessionStorage.getItem("language_selected");
        else
          return "en";
      }
    },
    mounted(){
      this.selected_language = this.getSelectedLanguage();
      this.$i18n.locale = this.selected_language;
    }
  }
</script>