const FirstStep = (props) => {
    
    const useStyles = makeStyles((theme: any) =>
        createStyles({
            root: {
                '& .MuiTextField-root': {
                    margin: theme.spacing(1),
                    width: 200,
                },
            },
        }),
    );
    const classes = useStyles();
    const [openAlertGreen, setOpenAlertGreen] = React.useState(false);
    const [openAlertRed, setOpenAlertRed] = React.useState(false);
    const form = useFormContext();
    //console.log(form);
    //const { reset, register } = form;
    const selectedCompany = form.watch('company');
    const availableObjectTypes = props.selectOptionsTypes.filter(type => type.companyId == selectedCompany)

    const handleClose = () => {
        setOpenAlertGreen(false);
        setOpenAlertRed(false);
    };
    return (
        <div>
            <form className={classes.root} autoComplete="off">
                <MySelect required 
                          selectOptions={props.selectOptionsСompanies}
                          form={form}
                          name="company"
                          // setSelectedValue={selectedValue => {
                          //     props.setSelectedValueTypes(null);
                          // }} 
                          nameSwlect="Выберите компанию"
                />
                <MySelect required 
                          name="objectTypeId" 
                          selectOptions={availableObjectTypes} 
                          form={form}
                          setSelectedValue={props.setSelectedValueTypes}
                          nameSwlect="Выберите тип объекта"
                />
            </form>
            <div>
                <FormDialogObjectType
                    form={form}
                    setOpenAlertRed={setOpenAlertRed}
                    setOpenAlertGreen={setOpenAlertGreen}
                    setObjectTypes={props.setObjectTypes}
                    selectOptionsTypes={props.selectOptionsTypes}
                    selectOptions={props.selectOptionsСompanies}
                />
            </div>

            <Snackbar open={openAlertGreen} autoHideDuration={6000} onClose={handleClose}>
                <div className="alert alert-success" role="alert">
                    This is a success alert with
                    <button type="button" className="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </Snackbar>

            <Snackbar open={openAlertRed} autoHideDuration={6000} onClose={handleClose}>
                <div className="alert alert-danger" role="alert">
                    This is a danger alert with
                    <button type="button" className="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </Snackbar>
        </div>
    );
}