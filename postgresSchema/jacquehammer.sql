/* ngo table */
CREATE TABLE ngo (
    id INT PRIMARY KEY NOT NULL,
    ein INT UNIQUE,
    name STRING(MAX) NOT NULL,
    einRegistrar STRING(MAX),
    addressLine1 STRING(MAX) NOT NULL,
    addressLine2 STRING(MAX),
    city STRING(MAX) NOT NULL,
    state STRING(2) NOT NULL,
    zipcode STRING(12) NOT NULL,
    phone STRING(12) NOT NULL,
    fax STRING(12),
    email STRING(MAX),
    website STRING(MAX)
);

/* ngoDetails table */
CREATE TABLE ngoDetails (
    annualIncome NUMERIC NOT NULL,
    hasKnownFundingSources BOOLEAN NOT NULL
);

/* ngoServices table */
CREATE TABLE ngoServices (
    id INT NOT NULL,
    maxShelter INT,
    providesShelter BOOLEAN NOT NULL,
    providesMedical BOOLEAN NOT NULL,
    providesDental BOOLEAN NOT NULL,
    providesLegal BOOLEAN NOT NULL,
    providesTraining BOOLEAN NOT NULL
);

/* requestedNeeds as Enumeration to be used as a row in a */
CREATE TYPE requestedNeeds AS ENUM (
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