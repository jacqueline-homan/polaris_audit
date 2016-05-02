/*
 * create database
 */
CREATE DATABASE IF NOT EXISTS jacquehammer WITH
    ENCODING = "UTF-8",
    TABLESPACE = "pg_default",
    ALLOW_CONNECTIONS = true,
    IS_TEMPLATE = true;


/*
 * ngo table
 */
CREATE TABLE IF NOT EXISTS ngo (
    id SERIAL primary key,
    ein INT UNIQUE,
    ngoName VARCHAR NOT NULL,
    einRegistrar VARCHAR,
    addressLine1 VARCHAR NOT NULL,
    addressLine2 VARCHAR,
    city VARCHAR NOT NULL,
    state VARCHAR(2) NOT NULL,
    zipcode VARCHAR(12) NOT NULL,
    phone VARCHAR(12) NOT NULL,
    fax VARCHAR(12),
    email VARCHAR,
    website VARCHAR
);


/*
 * ngodetails table
 */
CREATE TABLE IF NOT EXISTS ngodetails (
    id SERIAL primary key,
    annualIncome NUMERIC NOT NULL,
    hasKnownFundingSources BOOLEAN NOT NULL
);


/*
 * ngoservices table
 */
CREATE TABLE IF NOT EXISTS ngoservices (
    id SERIAL primary key,
    maxShelter INT,
    providesShelter BOOLEAN NOT NULL,
    providesMedical BOOLEAN NOT NULL,
    providesDental BOOLEAN NOT NULL,
    providesLegal BOOLEAN NOT NULL,
    providesTraining BOOLEAN NOT NULL
);


/*
 * callertype may be added to in the future, so perhaps
 * this should be a table
 */
create table if not exists callertype (
    id SERIAL primary key,
    typename varchar
);
insert into callerType (typename) values
    ('Survivor'),
    ('Advocate');


/*
 * ngotype table
 */
CREATE TABLE IF NOT EXISTS ngotype (
    id SERIAL primary key,
    typename VARCHAR
);
INSERT INTO ngotype (typename) VALUES
    ('Other'),
    ('Victim Safe House'),
    ('Homeless Shelter'),
    ('Poverty Relief'),
    ('Medical Dental Care'),
    ('Survivor Aid');


/*
 * requested needs changed to table for future needs
 */
create table if not exists requestedneeds (
    id SERIAL primary key,
    needsname varchar
);
insert into requestedneeds (needsname) values
    ('Undefined'),
    ('Legal'),
    ('Dental'),
    ('Medical'),
    ('Vison'),
    ('Hearing'),
    ('TraumaTherapy'),
    ('IncomeSupport'),
    ('PermanentHousing'),
    ('EducationHelp'),
    ('SkillsTraining'),
    ('JobPlacement');


/*
 * result of the visit with the ngo
 */
create table if not exists helpprovided (
    id SERIAL PRIMARY KEY,
    name varchar
);
insert into helpprovided (name) values
    ('Helped'),
    ('Partially Helped'),
    ('Ran Out of Options'),
    ('Not Helped'),
    ('Given the Wrong Help'),
    ('Referred');


/*
 * entry submitted for every step in the form
 */
create table if not exists callerentry (
    id SERIAL primary key,
    ngo integer references ngo (id),
    referrer integer references ngo (id),
    callertype integer references callertype (id) not null,
    ngotype integer references ngotype (id) not null,
    helpprovided integer references helpprovided (id),
    didgetfollowup boolean not null
);


/*
 * many to many relationship table for
 */
CREATE TABLE IF NOT EXISTS callerentries_to_requestedneeds (
    id SERIAL primary key,
    callerentry integer references callerentry (id),
    requestedneed integer references requestedneeds (id)
);






