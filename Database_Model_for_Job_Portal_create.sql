-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2022-04-21 13:35:39.672

-- tables
-- Table: company
CREATE TABLE Company (
    id int  NOT NULL,
    company_name varchar(100)  NOT NULL,
    profile_description varchar(1000)  NOT NULL,
    establishment_date date  NOT NULL,
    company_website_url varchar(500)  NOT NULL,
    company_logo_url varchar(500)  NOT NULL,
    CONSTRAINT company_pk PRIMARY KEY (id)
);

-- Table: education_detail
CREATE TABLE EducationDetail (
    user_account_id int  NOT NULL,
    certificate_degree_name varchar(50)  NOT NULL,
    major varchar(50)  NOT NULL,
    Institute_university_name varchar(50)  NOT NULL,
    CONSTRAINT education_detail_pk PRIMARY KEY (user_account_id,certificate_degree_name,major)
);

-- Table: experience_detail
CREATE TABLE ExperienceDetail (
    user_account_id int  NOT NULL,
    start_date date  NOT NULL,
    end_date date  NOT NULL,
    job_title varchar(50)  NOT NULL,
    company_name varchar(100)  NOT NULL,
    description varchar(4000)  NOT NULL,
    CONSTRAINT experience_detail_pk PRIMARY KEY (user_account_id,start_date,end_date)
);

-- Table: job_post
CREATE TABLE JobPost (
    id int  NOT NULL,
    job_type_id int NOT NULL,
    company_id int  NOT NULL,
    created_date date  NOT NULL,
    job_description varchar(500)  NOT NULL,
    job_location varchar(100)  NOT NULL,
    CONSTRAINT job_post_pk PRIMARY KEY (id)
);

-- Table: job_post_activity
CREATE TABLE JobPostActivity (
    user_account_id int  NOT NULL,
    job_post_id int  NOT NULL,
    apply_date date  NOT NULL,
    CONSTRAINT job_post_activity_pk PRIMARY KEY (user_account_id,job_post_id)
);

-- Table: job_type
CREATE TABLE JobType (
    id int  NOT NULL,
    job_type varchar(20)  NOT NULL,
    CONSTRAINT job_type_pk PRIMARY KEY (id)
);

-- Table: seeker_profile
CREATE TABLE SeekerProfile (
    user_account_id int  NOT NULL,
    first_name varchar(50)  NOT NULL,
    last_name varchar(50)  NOT NULL,
    CONSTRAINT seeker_profile_pk PRIMARY KEY (user_account_id)
);

-- Table: user_account
CREATE TABLE UserAccount (
    id int  NOT NULL,
    user_type_id int  NOT NULL,
    email varchar(255)  NOT NULL,
    password varchar(100)  NOT NULL,
    date_of_birth date  NOT NULL,
    gender char(1)  NOT NULL,
    contact_number varchar(10)  NOT NULL,
    user_image_url varchar(500)  NULL,
    registration_date date  NOT NULL,
    CONSTRAINT user_account_pk PRIMARY KEY (id)
);

-- Table: user_type
CREATE TABLE UserType (
    id int  NOT NULL,
    user_type_name varchar(20)  NOT NULL,
    CONSTRAINT user_type_pk PRIMARY KEY (id)
);

-- foreign keys
-- Reference: educ_dtl_seeker_profile (table: education_detail)
ALTER TABLE EducationDetail ADD CONSTRAINT educ_dtl_seeker_profile
    FOREIGN KEY (user_account_id)
    REFERENCES SeekerProfile (user_account_id)  
   
;

-- Reference: exp_dtl_seeker_profile (table: experience_detail)
ALTER TABLE ExperienceDetail ADD CONSTRAINT exp_dtl_seeker_profile
    FOREIGN KEY (user_account_id)
    REFERENCES SeekerProfile (user_account_id)  
;

-- Reference: job_post_act_user_register (table: job_post_activity)
ALTER TABLE JobPostActivity ADD CONSTRAINT job_post_act_user_register
    FOREIGN KEY (user_account_id)
    REFERENCES UserAccount (id)  
;

-- Reference: job_post_activity_job_post (table: job_post_activity)
ALTER TABLE JobPostActivity ADD CONSTRAINT job_post_activity_job_post
    FOREIGN KEY (job_post_id)
    REFERENCES JobPost (id)  
;

-- Reference: job_post_company (table: job_post)
ALTER TABLE JobPost ADD CONSTRAINT job_post_company
    FOREIGN KEY (company_id)
    REFERENCES Company (id)  
;

-- Reference: job_post_job_type (table: job_post)
ALTER TABLE JobPost ADD CONSTRAINT job_post_job_type
    FOREIGN KEY (job_type_id)
    REFERENCES JobType (id)  
;

-- Reference: seeker_profile_user_register (table: seeker_profile)
ALTER TABLE SeekerProfile ADD CONSTRAINT seeker_profile_user_register
    FOREIGN KEY (user_account_id)
    REFERENCES UserAccount (id)  
   
;

-- Reference: user_register_user_type (table: user_account)
ALTER TABLE UserAccount ADD CONSTRAINT user_register_user_type
    FOREIGN KEY (user_type_id)
    REFERENCES UserType (id)  
;

-- End of file.

