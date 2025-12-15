alter TABLE public.h_appointment
add column hour int;

alter TABLE public.h_appointment
add column minute int;


create or replace VIEW public.v_appointment
AS 
SELECT ha.id,
    ha.appointment_date,
    ha.patient_id,
    ha.doctor_id,
    (mp.last_name::text || ' '::text) || mp.first_name::text AS patient_name,
    (md.last_name::text || ' '::text) || md.first_name::text AS doctor_name,
    ha.reason_to_visit,
    ha.status,
    case 
	    when ha.hour is not null then ha.hour::varchar || ':' || 
	    case 
	    	when ha.minute < 10 then '0' || ha.minute::varchar
	    	else ha.minute::varchar
	    end::varchar
    end::varchar as times
   FROM h_appointment ha
     LEFT JOIN m_patient mp ON ha.patient_id = mp.id
     LEFT JOIN m_doctor md ON ha.doctor_id = md.id;

alter TABLE public.m_account
add column hospital_id int;

alter TABLE public.m_account
add CONSTRAINT fk_account_hospital FOREIGN KEY (hospital_id) REFERENCES public.m_hospital(id);


