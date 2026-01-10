using System.Collections;
using medicalcare_mongodb.models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Storage.ValueConversion;

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
        public const string Billing = "Billing";
        public const string Treatment = "Treatment";
        public const string Prescription = "Prescription";
        public DbSet<Account> m_account{get;set;}
        public DbSet<Hospital> m_hospital{get;set;}
        public DbSet<TreatmentCategory> m_treatment_category{get;set;}
        public DbSet<MedicineType> m_medicine_type{get;set;}
        public DbSet<Department> m_department {get;init;}
        public DbSet<Medicine> m_medicine {get;init;}
        public DbSet<Patient> m_patient {get;init;}
        public DbSet<Doctor> m_doctor {get;init;}
        public DbSet<Appointment> h_appointment {get;init;}
        public DbSet<Prescription> h_prescription {get;init;}
        public DbSet<Treatment> m_treatment {get;init;}
        public DbSet<Billing> h_billing {get;init;}
        public DbSet<DurationType> m_duration_type {get;init;}
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
            modelBuilder.Entity<Treatment>().HasKey(a=> a.id);
            modelBuilder.Entity<Prescription>().HasKey(a=> a.id);
            modelBuilder.Entity<Billing>().HasKey(a=> a.id);
            modelBuilder.Entity<DurationType>().HasKey(a=> a.id);
        }

        public bool ExpireDate(DateTime expireDate, int expireDay)
        {
            DateTime today = DateTime.Now;

            // Calculate the difference in days
            TimeSpan timeRemaining = expireDate - today;
            int daysUntilExpiration = (int)Math.Floor(timeRemaining.TotalDays);
            return (daysUntilExpiration <= expireDay) ? false: true;
        }

        public async Task<IEnumerable> GetBillings()
        {
            var billings = await this.h_billing.ToArrayAsync();
            var patients = await this.m_patient.ToArrayAsync();
            var prescriptions = await this.h_prescription.ToArrayAsync();
            var medicines = await this.m_medicine.ToArrayAsync();
            var treatments = await this.m_treatment.ToArrayAsync();
            var treatmentCategories = await this.m_treatment_category.ToArrayAsync();

            var v_treatments = from t in treatments
            join tc in treatmentCategories on t?.category_id_guid equals tc.id_guid into tcGroup
            from tcDefault in tcGroup.DefaultIfEmpty()
            select new
            {
                billing_id_guid = t?.billing_id_guid,
                treatment_ammount = Convert.ToDecimal(tcDefault?.price??0) * Convert.ToDecimal(t?.quantity??0)
            } 
            into grp
            group grp by new
            {
                billing_id_guid = grp.billing_id_guid
            } 
            into grp
            select new
            {
                billing_id_guid = grp.Key.billing_id_guid,
                treatment_total = grp.Sum(li=>li?.treatment_ammount)
            };

            var v_prescriptions = from t in prescriptions
            join tc in medicines on t?.medicine_id_guid equals tc.id_guid into tcGroup
            from tcDefault in tcGroup.DefaultIfEmpty()
            select new
            {
                billing_id_guid = t?.billing_id_guid,
                prescription_ammount = Convert.ToDecimal(tcDefault?.price??0) * Convert.ToDecimal(t?.quantity??0)
            } 
            into grp
            group grp by new
            {
                billing_id_guid = grp.billing_id_guid
            } 
            into grp
            select new
            {
                billing_id_guid = grp.Key.billing_id_guid,
                prescription_total = grp.Sum(li=>li?.prescription_ammount)
            };

            var v_billing = from b in billings
            join p in patients on b?.patient_id_guid equals p.id_guid into pGroup
            from pDefault in pGroup.DefaultIfEmpty()
            join t in v_treatments on b.id_guid equals t.billing_id_guid into tGroup
            from tDefault in tGroup.DefaultIfEmpty()
            join pr in v_prescriptions on b.id_guid equals pr.billing_id_guid into prGroup
            from prDefault in prGroup.DefaultIfEmpty()
            select new
            {
                billing_id = b.id_guid,
                billing_date = b.billing_date,
                patient_name = $"{pDefault?.last_name} {pDefault?.first_name}",
                hospital_id = pDefault?.hospital_id_guid,
                total = Convert.ToDecimal(prDefault?.prescription_total??0) + 
                    Convert.ToDecimal(tDefault?.treatment_total??0)
            };
            return v_billing;
        }

        public IDictionary? GetPrescriptionItem(Dictionary<string, object> item)
        {
            var medicine_id =  item["medicine_id_guid"]?.ToString();
            var id =  item["id"]?.ToString();
            var new_id =  item["new_id"]?.ToString();

            if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(new_id))
                new_id = Guid.NewGuid().ToString();
            else if (!string.IsNullOrEmpty(id))
                new_id = id;
                
            var billing_id = item["billing_id"]?.ToString();
            var quantity = Convert.ToInt32(item["quantity"].ToString());
            var medicine = m_medicine.Where(li => li.id == new ObjectId(medicine_id)).FirstOrDefault<Medicine?>();
            var medicine_name = medicine?.name??"";
            var price = medicine?.price??0;
            var medicine_type = item["medicine_type"].ToString();
            var duration_type = item["duration_type"].ToString();
            var duration = Convert.ToInt32(item["duration"].ToString());
            var prescription_date = Convert.ToDateTime(item["prescription_date"].ToString());
            var dosage = item["dosage"].ToString();
            var notes = item["notes"].ToString();
            var newItem = new Dictionary<string, object>
            {
                ["id"]  = id??"",
                ["new_id"] = new_id??"",
                ["billing_id"]  = billing_id??"",
                ["medicine_id"]  = medicine_id??"",
                ["medicine_name"]  = medicine_name,
                ["quantity"]  = quantity,
                ["amount"]  = price,
                ["total"]  = price * quantity,
                ["prescription_date"]  = prescription_date,
                ["medicine_type"] = medicine_type??"",
                ["duration"] = duration,
                ["duration_type"] = duration_type??"",
                ["dosage"] = dosage??"",
                ["notes"] = notes??""
            };
            return newItem;
        }

        public IDictionary? GetTreatmentItem(Dictionary<string, object> item)
        {
            var id =  item["id"]?.ToString();
            var new_id =  item["new_id"]?.ToString();

            if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(new_id))
            {
                new_id = Guid.NewGuid().ToString();
            }
            else if (!string.IsNullOrEmpty(id))
            {
                new_id = id;
            }

            var billing_id = item["billing_id_guid"]?.ToString();
            var category_id = item["category_id_guid"]?.ToString();

            var quantity = Convert.ToInt32(item["quantity"].ToString());
            var description = item["description"].ToString();
            var treatment_date = Convert.ToDateTime(item["treatment_date"].ToString());
            var categoryItem = m_treatment_category.Where(li=> li.id == new ObjectId(category_id)).FirstOrDefault();
            decimal? price = 0M;
            decimal? total = 0M;
            var treatment_type = "";
            if (categoryItem != null)
            {
                price = categoryItem?.price;
                total = price * quantity;
                treatment_type = categoryItem?.name_en;
            }
            var newItem = new Dictionary<string, object>
            {
                ["id"] = id??"",
                ["new_id"] = new_id??"",
                ["billing_id"]  = billing_id??"",
                ["quantity"]  = quantity,
                ["amount"]  = price??0,
                ["total"]  = total??0,
                ["treatment_date"]  = treatment_date,
                ["description"] = description??"",
                ["treatment_type"] = treatment_type??"",
                ["category_id"] = category_id??""
            };
            return newItem;
        }
        public async Task<IEnumerable> GetMedicines()
        {
            var medicines = await m_medicine.ToListAsync();
            var categories = await m_medicine_type.ToListAsync();

            var leftJoinResults = from m in medicines
            join c in categories on m.category_id_guid equals c.id_guid into categoryGroup
            from cDefault in categoryGroup.DefaultIfEmpty()
            select new
            {
                category_id = m.category_id_guid,
                medicine_type = cDefault?.name_en,
                name = m.name,
                price = m.price,
                id = m.id_guid
            };
            return leftJoinResults;
        }

        public async Task<IEnumerable> GetPatients()
        {
            var patients = await m_patient.ToListAsync();
            var hospitals = await m_hospital.ToListAsync();
            
            var leftJoinResults = from p in patients
            join h in hospitals on p.hospital_id_guid equals h.hospital_id_guid into hospitalGroup
            from hDefault in hospitalGroup.DefaultIfEmpty()
            select new
            {
                hospital_id = p.hospital_id_guid,
                hospital_name = hDefault?.name,
                code = p.code,
                date_of_birth = p.date_of_birth,
                first_name = p.first_name,
                last_name = p.last_name,
                gender = p.gender,
                home_address = p.home_address,
                office_address = p.office_address,
                phone_number = p.phone_number,
                email = p.email,
                job =  p.job,
                emergency_contact_name =  p.emergency_contact_name,
                emergency_contact_phone = p.emergency_contact_phone,
                insurance_type = p.insurance_type,
                insurance_policy_number = p.insurance_policy_number,
                insurance_provider = p.insurance_provider,
                insurance_expire = p.insurance_expire,
                insurance_info = p.insurance_info,
                medical_history = p.medical_history,
                id = p.id_guid
            };
            return leftJoinResults;
        }

        public async Task<IEnumerable> GettreatmentCategories()
        {
            var treatment_categories = await m_treatment_category.ToListAsync();

            var results = from m in treatment_categories
            select new
            {
                id = m.id_guid,
                m.name_en,
                m.name_vn,
                m.name_jp,
                m.description,
                m.price
            };
            return results;
        }

        public async Task<IEnumerable> GetMedicineTypes()
        {
            var medicine_types = await m_medicine_type.ToListAsync();

            var results = from m in medicine_types
            select new
            {
                id = m.id_guid,
                m.name_en,
                m.name_vn,
                m.name_jp,
                m.description
            };
            return results;
        }

        public async Task<IEnumerable> GetDepartments()
        {

            var departments = await m_department.ToListAsync();
            var hospitals = await m_hospital.ToListAsync();

            var leftJoinResults = from d in departments
            join h in hospitals on d.hospital_id_guid equals h.hospital_id_guid into hospitalGroup
            from hDefault in hospitalGroup.DefaultIfEmpty()
            select new
            {
                hospital_id = d.hospital_id_guid,
                hospital_name = hDefault?.name,
                name = d.name,
                phone = d.phone,
                id = d.id_guid 
            };
            return leftJoinResults;
        }

        public async Task<IEnumerable> GetHospitals()
        {
            var medicine_types = await m_hospital.ToListAsync();

            var results = from m in medicine_types
            select new
            {
                id = m.hospital_id_guid,
                m.name,
                m.phone,
                m.address,
                m.email,
                m.description,
                m.country
            };
            return results;
        }

        public async Task<IEnumerable> GetAccounts()
        {
            var items = await m_account.ToListAsync();

            var results = from m in items
            select new
            {
                id = m.id_guid,
                hospital_id = m.hospital_id_guid,
                m.email,
                m.phone,
                m.address,
                m.gender,
                m.first_name,
                m.last_name,
                m.role,
                m.account_type,
                m.password,
                m.dob,
                m.create_date
            };
            return results;
        }

        public async Task<IEnumerable> GetDoctors()
        {
            var doctors = await m_doctor.ToListAsync();
            var hospitals = await m_hospital.ToListAsync();

            var leftJoinResults = from d in doctors
            join h in hospitals on d.hospital_id_guid equals h.hospital_id_guid into hospitalGroup
            from hDefault in hospitalGroup.DefaultIfEmpty()
            select new
            {
                hospital_id = d.hospital_id_guid,
                hospital_name = hDefault?.name,
                first_name = d.first_name,
                last_name = d.last_name,
                gender = d.gender,
                phone = d.phone,
                email = d.email,
                quanlification =  d.quanlification,
                job_specification =  d.job_specification,
                id = d.id_guid 
            };
            return leftJoinResults;
        }

        public async Task<IEnumerable> GetAppointments()
        {
            var appointments = await h_appointment.ToListAsync();
            var doctors = await m_doctor.ToListAsync();
            var patients = await m_patient.ToListAsync();

            var leftJoinResult = from a in appointments
                             join d in doctors on a.doctor_id_guid equals d.id_guid into doctorGroup
                             from cDefault in doctorGroup.DefaultIfEmpty()
                             join p in patients on a.patient_id_guid equals p.id_guid into patientGroup
                             from pDefault in patientGroup.DefaultIfEmpty()
                             select new
                             {
                                id = a.id_guid,
                                doctor_id = a.doctor_id_guid,
                                patient_id = a.patient_id_guid,
                                appointment_date = a.appointment_date,
                                reason_to_visit = a.reason_to_visit,
                                status = a.status,
                                times = (a.hour == null) ? "" : $"{a.hour}:{a?.minute?.ToString().PadLeft(2, '0')}",
                                doctor_name = $"{cDefault?.last_name} {cDefault?.first_name}"  ?? "",
                                patient_name = $"{pDefault?.last_name} {pDefault?.first_name}"  ?? "",
                                hospital_id = pDefault?.hospital_id_guid  ?? null
                             };
            return leftJoinResult;
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
            else if (controllerName.IndexOf(Billing, StringComparison.OrdinalIgnoreCase) != -1)
                return await h_billing.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(Treatment, StringComparison.OrdinalIgnoreCase) != -1)
                return await m_treatment.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
            else if (controllerName.IndexOf(Prescription, StringComparison.OrdinalIgnoreCase) != -1)
                return await h_prescription.FirstOrDefaultAsync(li => li.id == new ObjectId(id));
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
            else if (controllerName.IndexOf(Billing, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteBilling(id, controllerName);
            else if (controllerName.IndexOf(Treatment, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeleteTreatment(id, controllerName);
            else if (controllerName.IndexOf(Prescription, StringComparison.OrdinalIgnoreCase) != -1)
                return await DeletePrescription(id, controllerName);
            return false;
        }

        async Task<bool> DeleteBilling(string id, string controllerName)
        {
            var item = (Billing?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    h_billing.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }

        async Task<bool> DeleteTreatment(string id, string controllerName)
        {
            var item = (Treatment?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    m_treatment.Remove(item);
                    SaveChanges();
                });
                return true;
            }
        }

        async Task<bool> DeletePrescription(string id, string controllerName)
        {
            var item = (Prescription?) await GetItem(id, controllerName);
            if (item == null)
                return false;
            else
            {
                await Task.Run(() =>
                {
                    h_prescription.Remove(item);
                    SaveChanges();
                });
                return true;
            }
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
        public IDictionary? GetBillingDetails(string billing_id)
        {
            var billings = h_billing.Where(li => li.id == new ObjectId(billing_id)).ToList();
            var patient_id_guid = (billings != null) ? billings[0]?.patient_id_guid : null;

            var prescriptions = h_prescription.Where(li=> li.billing_id == new ObjectId(billing_id)).ToList();
            var treatments = m_treatment.Where(li=> li.billing_id == new ObjectId(billing_id)).ToList();
            var medicines = m_medicine.ToList();
            var doctors = m_doctor.ToList();

            var patients = m_patient.Where(li=> li.id == new ObjectId(patient_id_guid)).ToList();
            
            var patientItem = (patients != null && patients.Count > 0) ? patients[0]:null;
            var treatmentCategories = m_treatment_category.ToList();


            var appoitments = h_appointment.Where(li=> li.patient_id == new ObjectId(patient_id_guid)).ToList();


            var discharge_date = (billings != null) ? billings[0]?.discharge_date:null;

            var first_name = patientItem?.first_name??"";
            var last_name = patientItem?.last_name??"";

            var paItem = new Dictionary<string, object>
            {
                ["patient_id"] = patient_id_guid??"",
                ["patient_code"] = patientItem?.code??"",
                ["patient_name"] = $"{last_name} {first_name}",
                ["gender"] = patientItem?.gender??"",
                ["insurance_policy_number"] = patientItem?.insurance_policy_number??"",
                ["insurance_type"] = patientItem?.insurance_type??"",
                ["date_of_birth"] = patientItem?.date_of_birth??new DateTime(),
                ["insurance_expire"] = patientItem?.insurance_expire??new DateTime(),
                ["job"] = patientItem?.job??"",
                ["home_address"] = patientItem?.home_address??"",
                ["office_address"] = patientItem?.office_address??"",
            };

            var v_prescriptions = from pr in prescriptions
            join m in medicines on pr.medicine_id equals m.id
            select new
            {
                id = pr?.id_guid,
                medicine_id_guid = m?.id_guid,
                billing_id_guid = pr?.billing_id_guid,
                prescription_date = pr?.prescription_date,
                medicine_name = m?.name,
                quantity = pr?.quantity??0,
                amount = Convert.ToDecimal((m?.price??0)),
                total = Convert.ToDecimal((m?.price??0)) * Convert.ToDecimal(pr?.quantity??0)
            };

            
            var v_treatments = from t in treatments
            join tc in treatmentCategories on t.category_id equals tc.id
            select new
            {
                id = t?.id_guid,
                category_id_guid = tc?.id_guid,
                billing_id_guid = t?.billing_id_guid,
                treatment_date = t?.treatment_date,
                treatment_type = tc?.name_en,
                quantity = t?.quantity??0,
                amount = Convert.ToDecimal((tc?.price??0)),
                total = Convert.ToDecimal((tc?.price??0)) * Convert.ToDecimal(t?.quantity??0)
            };

            if (billings != null)
            {
                var v_treatment = from t in v_treatments
                select new
                {
                    billing_id_guid = t.billing_id_guid,
                    treatment_ammount = t.total
                }
                into grp
                group grp by new
                {
                    billing_id_guid = grp.billing_id_guid
                } 
                into grp
                select new
                {
                    billing_id_guid = grp.Key.billing_id_guid,
                    treatment_total = grp.Sum(li=>li?.treatment_ammount)
                };

                var v_prescription = from t in v_prescriptions
                select new
                {
                    billing_id_guid = t.billing_id_guid,
                    prescription_total = t.total
                }
                into grp
                group grp by new
                {
                    billing_id_guid = grp.billing_id_guid
                } 
                into grp
                select new
                {
                    billing_id_guid = grp.Key.billing_id_guid,
                    prescription_total = grp.Sum(li=>li?.prescription_total)
                };

                var v_billing = from b in billings
                join t in v_treatment on b.id_guid equals t.billing_id_guid
                join pr in v_prescription on b.id_guid equals pr.billing_id_guid
                select new
                {
                    discharge_date = b.discharge_date,
                    admission_date = b.admission_date,
                    amount = Convert.ToDecimal(pr?.prescription_total??0) + 
                        Convert.ToDecimal(t?.treatment_total??0)
                };
                paItem["billing"] = v_billing;
            }

            if (patients != null)
            {
                var v_appointment = from ap in appoitments
                join pa in patients on ap.patient_id equals pa.id
                join doc in doctors on ap.doctor_id equals doc.id
                select new
                {
                    id = ap?.id_guid,
                    appointment_date = ap?.appointment_date,
                    patient_name = $"{pa?.last_name} {pa?.first_name}",
                    doctor_name = $"{doc?.last_name} {doc?.first_name}",
                    reason_to_visit = ap?.reason_to_visit,
                };
                paItem["appointments"] = v_appointment;
            }

            paItem["prescriptions"] = v_prescriptions;

            paItem["treatments"] = v_treatments;

            return paItem;
        }
        
        public IDictionary? GetMedicalDetails(
            string patient_id, 
            int admission_month,  
            int admission_year)
        {
            var item = m_patient.Where(li => li.id == new ObjectId(patient_id)).FirstOrDefault<Patient?>();

            var first_name = item?.first_name??"";
            var last_name = item?.last_name??"";
            var billings = h_billing.Where(li => li.patient_id == new ObjectId(patient_id)).ToList();
            var paItem = new Dictionary<string, object>
            {
                ["patient_id"] = patient_id,
                ["patient_code"] = item?.code??"",
                ["patient_name"] = $"{last_name} {first_name}",
                ["gender"] = item?.gender??"",
                ["insurance_policy_number"] = item?.insurance_policy_number??"",
                ["insurance_type"] = item?.insurance_type??"",
                ["date_of_birth"] = item?.date_of_birth??new DateTime(),
                ["insurance_expire"] = item?.insurance_expire??new DateTime(),
                ["job"] = item?.job??"",
                ["home_address"] = item?.home_address??"",
                ["office_address"] = item?.office_address??"",
            };

            var v_medicalcare = from b in billings
            join p in m_patient.ToArray() on b.patient_id equals p.id
            select new
            {
                billing_id = b.id_guid,
                patient_id = b.patient_id_guid,
                patient_name = $"{p.last_name} {p.first_name}",
                diagnostic = b.diagnostic,
                admission_date = b.admission_date,
                admission_month = (b.admission_date == null) ? 0: b.admission_date.Value.Month,
                admission_year = (b.admission_date == null) ? 0: b.admission_date.Value.Year
            };

            List<IDictionary> medicals = new List<IDictionary>();

            foreach (var medicalItem in v_medicalcare.Where(li=> li.admission_month == admission_month && li.admission_year == admission_year).ToList())
            {
                var billing_id = medicalItem?.billing_id??"";
                var it = new Dictionary<string, object>
                {
                    ["billing_id"] = billing_id,
                    ["diagnostic"] = medicalItem?.diagnostic??"",
                };

                var v_prescription = from p in h_prescription.ToArray()
                join b in billings on p.billing_id equals b.id
                join m in m_medicine.ToArray() on p.medicine_id equals m.id
                select new
                {
                    id = p.id_guid,
                    patient_id = b.patient_id_guid,
                    billing_id = p.billing_id_guid,
                    p.dosage,
                    p.quantity,
                    p.duration,
                    p.duration_type,
                    p.prescription_date,
                    p.notes,
                    medicine_name = m.name,
                    medicine = p.medicine_type
                };
                var presItems = v_prescription.Where(li => li.patient_id == patient_id).ToList();
                List<IDictionary> prescriptions = new List<IDictionary>();
                if (presItems != null)
                {
                    foreach (var presItem in presItems)
                    {
                        var dicPres = new Dictionary<string, object>
                        {
                            ["prescription_id"] = presItem?.id??"",
                            ["dosage"] = presItem?.dosage??"",
                            ["quantity"] = presItem?.quantity??0,
                            ["duration"] = presItem?.duration??0,
                            ["duration_type"] = presItem?.duration_type??"",
                            ["medicine"] = presItem?.medicine??"",
                            ["prescription_date"] = presItem?.prescription_date?? DateTime.Today,
                            ["notes"] = presItem?.notes??"",
                            ["medicine_name"] = presItem?.medicine_name??"",
                        };
                        prescriptions.Add(dicPres);
                    }
                }
                it["prescriptions"] = prescriptions;

                var v_treatment = from t in m_treatment.ToArray()
                join b in billings on t.billing_id equals b.id
                join tc in m_treatment_category.ToArray() on t.category_id equals tc.id
                select new
                {
                    id = t.id_guid,
                    patient_id = b.patient_id_guid,
                    billing_id = t.billing_id_guid,
                    t.description,
                    t.treatment_date,
                    treatment_type = tc.name_en
                };
                var treatmentItems = v_treatment.Where(li => li.patient_id == patient_id).ToList();
                
                List<IDictionary> treatments = new List<IDictionary>();
                
                if (treatmentItems != null)
                {
                    // treatmentItems = treatmentItems.
                    //     OrderBy(li=> li.billing_id).
                    //     ThenBy(li=> li.billing_id);

                    foreach (var treatmentItem in treatmentItems)
                    {
                        var dicTreatment = new Dictionary<string, object>
                        {
                            ["treatment_id"] = treatmentItem?.id??"",
                            ["treatment_type"] = treatmentItem?.treatment_type??"",
                            ["description"] = treatmentItem?.description??"",
                            ["treatment_date"] = treatmentItem?.treatment_date?? DateTime.Today,
                        };
                        treatments.Add(dicTreatment);
                    }
                    DateTime? start_date = treatmentItems?.First()?.treatment_date??null;
                    DateTime? end_date = (treatmentItems?.Count() > 1) ? treatmentItems?.Last()?.treatment_date??null: null;
                    it["start_date"] = start_date??DateTime.Today;
                    it["end_date"] = end_date??DateTime.Today;
                }
                
                it["treatments"] = treatments;

                medicals.Add(it);
            }
            paItem["medical"] = medicals;

            return paItem;
        }
    }
}