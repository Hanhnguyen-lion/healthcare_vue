alter table m_patient
add column hospital_id int;

alter table m_patient
add constraint fk_patient_hospital FOREIGN KEY (hospital_id) REFERENCES public.m_hospital(id);

CREATE OR REPLACE VIEW public.v_billing
AS SELECT hb.id AS billing_id,
    hb.patient_id,
    mp.code AS patient_code,
    format('%s %s'::text, mp.last_name, mp.first_name) AS patient_name,
    hb.billing_date,
    sum(hp.quantity::numeric * mm.price + mt.quantity::numeric * tc.price) AS total,
    mp.hospital_id
   FROM h_billing hb
     LEFT JOIN m_patient mp ON hb.patient_id = mp.id
     LEFT JOIN h_prescription hp ON hp.billing_id = hb.id
     LEFT JOIN m_medicine mm ON hp.medicine_id = mm.id
     LEFT JOIN m_treatment mt ON mt.billing_id = hb.id
     LEFT JOIN m_treatment_category tc ON mt.category_id = tc.id
  GROUP BY 
  	hb.id, 
  	hb.patient_id, 
  	mp.code, 
  	mp.last_name, 
  	mp.first_name, 
  	hb.billing_date,
  	mp.hospital_id
  ORDER BY hb.id;

CREATE OR REPLACE VIEW public.v_medicalcare
AS SELECT hb.id,
    hb.patient_id,
    mp.code AS patient_code,
    format('%s %s'::text, mp.last_name, mp.first_name) AS patient_name,
    hb.admission_date,
    date_part('month'::text, hb.admission_date)::integer AS admission_month,
    date_part('year'::text, hb.admission_date)::integer AS admission_year,
    hb.diagnostic,
    hb.notes,
    hb.department_id,
    mdep.name AS department_name,
    hb.doctor_id,
    format('%s %s'::text, md.last_name, md.first_name) AS doctor_name,
    mp.hospital_id
  FROM h_billing hb
     LEFT JOIN m_patient mp ON hb.patient_id = mp.id
     LEFT JOIN m_doctor md ON hb.doctor_id = md.id
     LEFT JOIN m_department mdep ON hb.department_id = mdep.id
  ORDER BY mp.code, hb.admission_date;

-- public.v_appointment source

CREATE OR REPLACE VIEW public.v_appointment
AS SELECT ha.id,
    ha.appointment_date,
    ha.patient_id,
    ha.doctor_id,
    (mp.last_name::text || ' '::text) || mp.first_name::text AS patient_name,
    (md.last_name::text || ' '::text) || md.first_name::text AS doctor_name,
    ha.reason_to_visit,
    ha.status,
        CASE
            WHEN ha.hour IS NOT NULL THEN (ha.hour::character varying::text || ':'::text) ||
            CASE
                WHEN ha.minute < 10 THEN ('0'::text || ha.minute::character varying::text)::character varying
                ELSE ha.minute::character varying
            END::text
            ELSE NULL::text
        END::character varying AS times,
    mp.hospital_id
   FROM h_appointment ha
     LEFT JOIN m_patient mp ON ha.patient_id = mp.id
     LEFT JOIN m_doctor md ON ha.doctor_id = md.id;
