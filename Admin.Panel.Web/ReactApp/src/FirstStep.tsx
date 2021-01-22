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
    const firstStepForm = useForm();
    
    const selectedCompany = firstStepForm.watch('company');
    const availableObjectTypes = props.selectOptionsTypes.filter(type => type.companyId == selectedCompany)

    const handleClose = () => {
        setOpenAlertGreen(false);
        setOpenAlertRed(false);
    };
    return (
        <div>
            <form className={classes.root} autoComplete="off">
                <MySelect required selectOptions = {props.selectOptionsСompanies} selectedValue = {props.selectedValueСompanies} form={firstStepForm}
                          name="company"
                          setSelectedValue = {selectedValue => {
                              props.setSelectedValueTypes(null);
                              props.setSelectedValueСompanies(selectedValue);
                          }} nameSwlect = "Выберите компанию"/>
                <MySelect required name="objectType" selectOptions = {availableObjectTypes} form={firstStepForm}
                          selectedValue = {props.selectedValueTypes} setSelectedValue = {props.setSelectedValueTypes} nameSwlect = "Выберите тип объекта" />
            </form>
            <div>
                <FormDialogObjectType
                    form={firstStepForm}
                    setOpenAlertRed={setOpenAlertRed}
                    setOpenAlertGreen = {setOpenAlertGreen}
                    setObjectTypes = {props.setObjectTypes}
                    selectOptionsTypes = {props.selectOptionsTypes}
                selectOptions = {props.selectOptionsСompanies} 
                selectedValue = {props.selectedValueСompanies} 
                setSelectedValue = {props.setSelectedValueСompanies}/>
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