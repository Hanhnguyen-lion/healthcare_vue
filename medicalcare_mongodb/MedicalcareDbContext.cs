using System.Collections;
using medicalcare_mongodb.models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb
{
    public class MedicalcareDbContext: DbContext
    {
        public const string Appointment = "Appointment";
        public const string Account = "Account";
        public const string Doctor = "Doctor";
        public const string Department = "Department";
        public const string Hospital = "Hospital";
        public const string TreatmentsCategory = "TreatmentsCategory";
        public const string MedicinesCategory = "MedicinesCategory";
        public const string Medicine = "Medicine";
        public const string Patient = "Patient";
        public DbSet<Account> m_account{get;set;}
        public DbSet<Hospital> m_hospital{get;set;}
        public DbSet<TreatmentCategory> m_treatment_category{get;set;}
        public DbSet<MedicineType> m_medicine_type{get;set;}
        public DbSet<Department> m_department {get;init;}
        public DbSet<Medicine> m_medicine {get;init;}
        public DbSet<Patient> m_patient {get;init;}
        public DbSet<Doctor> m_doctor {get;init;}
        public DbSet<Appointment> h_appointment {get;init;}
        public MedicalcareDbContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().HasKey(a=> a.id);
            modelBuilder.Entity<Hospital>().HasKey(a=> a.id);
            modelBuilder.Entity<MedicineType>().HasKey(a=> a.id);
            modelBuilder.Entity<TreatmentCategory>().HasKey(a=> a.id);
            modelBuilder.Entity<Department>().HasKey(a=> a.id);
            modelBuilder.Entity<Medicine>().HasKey(a=> a.id);
            modelBuilder.Entity<Patient>().HasKey(a=> a.id);
            modelBuilder.Entity<Doctor>().HasKey(a=> a.id);
            modelBuilder.Entity<Appointment>().HasKey(a=> a.id);
        }

        public async Task<IEnumerable> GetMedicines()
        {
            var medicines = await m_medicine.ToListAsync();
            var categories = await m_medicine_type.ToListAsync();

            var newItems = medicines.GroupJoin(categories,
                medicine=> medicine.category_id_guid,
                category=> category.id_guid,
                (medicine, category) => new
                {medicine, category}
            ).SelectMany(
                x=> x.category.DefaultIfEmpty(),
            (medicine, category) => new {
                    category_id = medicine.medicine.category_id,
                    category_id_guid = medicine.medicine.category_id_guid,
                    medicine_type = category?.name_en,
                    name = medicine.medicine.name,
                    price = medicine.medicine.price,
                    id_guid = medicine.medicine.id_guid,
                    id = medicine.medicine.id
                }
            );
            return newItems;
        }

        public async Task<IEnumerable> GetPatients()
        {
            var patients = await m_patient.ToListAsync();
            var hospitals = await m_hospital.ToListAsync();

            var newItems = patients.GroupJoin(hospitals,
                patient=> patient.hospital_id_guid,
                hospital=> hospital.hospital_id_guid,
                (patient, hospital) => new
                {patient, hospital}
            ).SelectMany(
                x=> x.hospital.DefaultIfEmpty(),
                    (patient, hospital) => 
                    new {
                            hospital_id = patient.patient.hospital_id,
                            hospital_id_guid = patient.patient.hospital_id_guid,
                            hospital_name = hospital?.name,
                            code = patient.patient.code,
                            date_of_birth = patient.patient.date_of_birth,
                            first_name = patient.patient.first_name,
                            last_name = patient.patient.last_name,
                            gender = patient.patient.gender,
                            home_address = patient.patient.home_address,
                            office_address = patient.patient.office_address,
                            phone_number = patient.patient.phone_number,
                            email = patient.patient.email,
                            job =  patient.patient.job,
                            emergency_contact_name =  patient.patient.emergency_contact_name,
                            emergency_contact_phone = patient.patient.emergency_contact_phone,
                            insurance_type = patient.patient.insurance_type,
                            insurance_policy_number = patient.patient.insurance_policy_number,
                            insurance_provider = patient.patient.insurance_provider,
                            insurance_expire = patient.patient.insurance_expire,
                            insurance_info = patient.patient.insurance_info,
                            medical_history = patient.patient.medical_history,
                            id_guid = patient.patient.id_guid,
                            id = patient.patient.id
                        }
                    );
            return newItems;
        }

        public async Task<IEnumerable> GetDoctors()
        {
            var doctors = await m_doctor.ToListAsync();
            var hospitals = await m_hospital.ToListAsync();

            var newItems = doctors.GroupJoin(hospitals,
                doctor=> doctor.hospital_id_guid,
                hospital=> hospital.hospital_id_guid,
                (doctor, hospital) => new
                {doctor, hospital}
            ).SelectMany(
                x=> x.hospital.DefaultIfEmpty(),
                    (doctor, hospital) => 
                    new {
                            hospital_id = doctor.doctor.hospital_id,
                            hospital_id_guid = doctor.doctor.hospital_id_guid,
                            hospital_name = hospital?.name,
                            first_name = doctor.doctor.first_name,
                            last_name = doctor.doctor.last_name,
                            gender = doctor.doctor.gender,
                            phone = doctor.doctor.phone,
                            email = doctor.doctor.email,
                            quanlification =  doctor.doctor.quanlification,
                            job_specification =  doctor.doctor.job_specification,
                            id_guid = doctor.doctor.id_guid,
                            id = doctor.doctor.id
                        }
                    );
            return newItems;
        }

        public async Task<IEnumerable> GetAppointments()
        {
            var appointments = await h_appointment.ToListAsync();
            var doctors = await m_doctor.ToListAsync();
            var patients = await m_patient.ToListAsync();

            var leftJoinResult = from a in appointments
                             // Left Join appointments and doctors
                             join d in doctors on a.doctor_id_guid equals d.id_guid into doctorGroup
                             from cDefault in doctorGroup.DefaultIfEmpty()
                             // Left Join appointments and patients (joining on p.Id again)
                             join p in patients on a.patient_id_guid equals p.id_guid into patientGroup
                             from pDefault in patientGroup.DefaultIfEmpty()
                             select new
                             {
                                id = a.id,
                                id_guid = a.id_guid,
                                doctor_id = a.doctor_id,
                                doctor_id_guid = a.doctor_id_guid,
                                patient_id = a.patient_id,
                                patient_id_guid = a.patient_id_guid,
                                appointment_date = a.appointment_date,
                                reason_to_visit = a.reason_to_visit,
                                status = a.status,
                                times = (a.hour == null) ? "" : $"{a.hour}:{a?.minute?.ToString().PadLeft(2, '0')}",
                                doctor_name = $"{cDefault?.last_name} {cDefault?.first_name}"  ?? "",
                                patient_name = $"{pDefault?.last_name} {pDefault?.first_name}"  ?? "",
                                hospital_id_guid = pDefault?.hospital_id_guid  ?? null,
                                hospital_id = pDefault?.hospital_id  ?? null
                             };
            return leftJoinResult;
        }

        public string CodePatient
        {
            get
            {
                var count = (m_patient == null) ? 1: m_patient.Count() + 1;
                var today = DateTime.Today.ToString("yyyyMMdd");
                return $"{today}_{count}";
            }
        }

        public async Task<Object?> GetItem(string id, string controllerName)
        {
            if (controllerName.IndexOf(Account, StringComparison.OrdinalIgnoreCase) != -1)
                return await m_account.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(Department, StringComparison.OrdinalIgnoreCase) != -1)
                return await m_department.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(Hospital, StringComparison.OrdinalIgnoreCase) != -1)
                return await m_hospital.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(MedicinesCategory, StringComparison.OrdinalIgnoreCase) != -1)
                return await m_medicine_type.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(TreatmentsCategory, StringComparison.OrdinalIgnoreCase) != -1)
                return await m_treatment_category.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(Medicine, StringComparison.OrdinalIgnoreCase) != -1)
                return await m_medicine.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(Patient, StringComparison.OrdinalIgnoreCase) != -1)
                return await m_patient.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(Doctor, StringComparison.OrdinalIgnoreCase) != -1)
                return await m_doctor.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(Appointment, StringComparison.OrdinalIgnoreCase) != -1)
                return await h_appointment.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            return null;
        }

        public async Task<bool> DeleteItem(string id, string controllerName)
        {
            if (controllerName.IndexOf(Account, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteAccount(id, controllerName);
            else if (controllerName.IndexOf(Department, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteDepartment(id, controllerName);
            else if (controllerName.IndexOf(Hospital, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteHospital(id, controllerName);
            else if (controllerName.IndexOf(TreatmentsCategory, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteTreatmentCategory(id, controllerName);
            else if (controllerName.IndexOf(MedicinesCategory, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteMedicineType(id, controllerName);
            else if (controllerName.IndexOf(Medicine, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteMedicine(id, controllerName);
            else if (controllerName.IndexOf(Patient, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeletePatient(id, controllerName);
            else if (controllerName.IndexOf(Doctor, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteDoctor(id, controllerName);
            else if (controllerName.IndexOf(Appointment, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteAppointment(id, controllerName);
            return false;
        }

        async Task<bool> DeleteAppointment(string id, string controllerName)
        {
            var item = (Appointment?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    h_appointment.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }

        async Task<bool> DeleteDoctor(string id, string controllerName)
        {
            var item = (Doctor?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    m_doctor.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }

        async Task<bool> DeletePatient(string id, string controllerName)
        {
            var item = (Patient?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    m_patient.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }
        async Task<bool> DeleteMedicine(string id, string controllerName)
        {
            var item = (Medicine?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    m_medicine.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }

        async Task<bool> DeleteAccount(string id, string controllerName)
        {
            var item = (Account?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    m_account.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }

        async Task<bool> DeleteMedicineType(string id, string controllerName)
        {
            var item = (MedicineType?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    m_medicine_type.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }

        async Task<bool> DeleteTreatmentCategory(string id, string controllerName)
        {
            var item = (TreatmentCategory?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    m_treatment_category.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }

        async Task<bool> DeleteHospital(string id, string controllerName)
        {
            var item = (Hospital?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    m_hospital.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }

        async Task<bool> DeleteDepartment(string id, string controllerName)
        {
            var item = (Department?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    m_department.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }
   }
}