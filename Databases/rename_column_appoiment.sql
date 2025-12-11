alter table public.h_appointment
RENAME COLUMN reason_to_vist TO reason_to_visit;

create or replace view public.v_appointment
as
select
	ha.id,
	ha.appointment_date,
	ha.patient_id,
	ha.doctor_id,
	mp.last_name || ' ' || mp.first_name as patient_name,
	md.last_name || ' ' || md.first_name as doctor_name,
	ha.reason_to_visit,
	ha.status
from h_appointment ha
left join public.m_patient mp on ha.patient_id = mp.id
left join public.m_doctor md on ha.doctor_id = md.id;
