create table ngoServices (
	ngo.id bigint not null, --foreign key to ngo.id
	maxShelter int,
	providesShelter boolean not_null,
	providesMedical boolean not_null,
	providesDental boolean not_null,
	providesLegal boolean not_null,
	providesTraining boolean not_null
)