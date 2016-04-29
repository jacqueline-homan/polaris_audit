
CREATE TABLE ngoServices (
	ngo.id BIGINT NOT NULL, --foreign key to ngo.id
	maxShelter int,
	providesShelter BOOLEAN not_null,
	providesMedical BOOLEAN not_null,
	providesDental BOOLEAN not_null,
	providesLegal BOOLEAN not_null,
	providesTraining BOOLEAN not_null
)