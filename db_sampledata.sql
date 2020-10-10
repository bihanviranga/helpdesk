USE db_helpdesk
GO

/** Company **/
insert into Tkt_Company values ("a3919d62-f453-4f4c-a104-a856a0f18f2c", "Helpdesk Company Ltd")
insert into Tkt_Company values ("0515da81-215e-4819-85db-9fff7603e0b7", "Client Company Ltd")
GO

/** Category **/
insert into Tkt_Category values(
	"SAMP_CAT_1",
	"a3919d62-f453-4f4c-a104-a856a0f18f2c",
	"Sample Category 1"
)
insert into Tkt_Category values(
	"SAMP_CAT_2",
	"0515da81-215e-4819-85db-9fff7603e0b7",
	"Sample Category 2"
)
GO

/** Brand **/
insert into Tkt_CompanyBrand values(
	"SAMP_BND_1",
	"a3919d62-f453-4f4c-a104-a856a0f18f2c",
	"Sample Brand 1"
)
insert into Tkt_CompanyBrand values(
	"SAMP_BND_2",
	"0515da81-215e-4819-85db-9fff7603e0b7",
	"Sample Brand 2"
)
GO

/** Module **/
insert into Tkt_Module values(
	"SAMP_MOD_1",
	"a3919d62-f453-4f4c-a104-a856a0f18f2c",
	"Sample Module 1"
)
insert into Tkt_Module values(
	"SAMP_MOD_2",
	"0515da81-215e-4819-85db-9fff7603e0b7",
	"Sample Module 2"
)
GO

/** Product **/
insert into Tkt_Product values(
	"SAMP_PROD_1",
	"a3919d62-f453-4f4c-a104-a856a0f18f2c",
	"Sample Product 1"
)
insert into Tkt_Product values(
	"SAMP_PROD_2",
	"0515da81-215e-4819-85db-9fff7603e0b7",
	"Sample Product 2"
)
GO

/** User **/
/** Ticket **/
/** Article **/
/** Conversation **/
/** Response Template **/
/** Ticket Operator **/
/** Ticket Timeline **/

