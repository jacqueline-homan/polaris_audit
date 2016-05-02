/*
 * create database
 */
CREATE DATABASE IF NOT EXISTS jacquehammer WITH
    ENCODING = "UTF-8",
    TABLESPACE = "pg_default",
    ALLOW_CONNECTIONS = true,
    IS_TEMPLATE = true;

COMMENT
ON DATABASE jacquehammer
IS "Added template capability for easy replications
in the future by non-admins"


/*
 * ngo table
 */
CREATE TABLE IF NOT EXISTS ngo (
    id SERIAL,
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
 * ngoDetails table
 */
CREATE TABLE IF NOT EXISTS ngodetails (
    id SERIAL,
    annualIncome NUMERIC NOT NULL,
    hasKnownFundingSources BOOLEAN NOT NULL
);


/*
 * ngoServices table
 */
CREATE TABLE IF NOT EXISTS ngoservices (
    id SERIAL,
    maxShelter INT,
    providesShelter BOOLEAN NOT NULL,
    providesMedical BOOLEAN NOT NULL,
    providesDental BOOLEAN NOT NULL,
    providesLegal BOOLEAN NOT NULL,
    providesTraining BOOLEAN NOT NULL
);


/*
 * requested needs changed to table for future needs
 */
create table if not exists requestedneeds (
    id SERIAL,
    needsname varchar
);
insert into requestedneeds values
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
 * callertype may be added to in the future, so perhaps
 * this should be a table
 */
create table if not exists callertype (
    id SERIAL,
    type varchar
);
insert into callerType (typeName) values
    ('Survivor'),
    ('Advocate');