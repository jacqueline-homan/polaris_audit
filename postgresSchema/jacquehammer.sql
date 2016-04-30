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
    id INT PRIMARY KEY NOT NULL,
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
CREATE TABLE IF NOT EXISTS ngoDetails (
    annualIncome NUMERIC NOT NULL,
    hasKnownFundingSources BOOLEAN NOT NULL
);


/*
 * ngoServices table
 */
CREATE TABLE IF NOT EXISTS ngoServices (
    id INT NOT NULL,
    maxShelter INT,
    providesShelter BOOLEAN NOT NULL,
    providesMedical BOOLEAN NOT NULL,
    providesDental BOOLEAN NOT NULL,
    providesLegal BOOLEAN NOT NULL,
    providesTraining BOOLEAN NOT NULL
);

/*
 * requestedNeeds as Enumeration to be used as a row in a
 * table
 */
CREATE TYPE requestedneeds AS ENUM (
    'Undefined',
    'Legal',
    'Dental',
    'Medical',
    'Vison',
    'Hearing',
    'TraumaTherapy',
    'IncomeSupport',
    'PermanentHousing',
    'EducationHelp',
    'SkillsTraining',
    'JobPlacement'
);