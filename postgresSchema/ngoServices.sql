create table ngoServices (
	ngo.id bigint not null, --foreign key to ngo.id
	maxShelter int,
	providesShelter boolean,
	providesMedical boolean,
	providesDental boolean,
	providesLegal boolean,
	providesTraining boolean
)