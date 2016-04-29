
CREATE TABLE ngo (
	BIGINT id primary_key AUTO_INCREMENT not_null,
	ein int UNIQUE,
	name string(MAX) not_null,
	einRegistrar string(MAX),
	addressLine1 string(MAX) not_null,
	addressLine2 string(MAX),
	city string(MAX) not_null,
	state string(2) not_null,
	zipcode string(12) not_null,
	phone string(12) not_null,
	fax string(12),
	email string(MAX),
	website string(MAX)
)
	
	