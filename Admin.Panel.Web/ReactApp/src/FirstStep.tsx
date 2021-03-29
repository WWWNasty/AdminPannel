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
    const { handleSubmit } = form;
    const selectedCompany = form.watch('companyId');
    const availableObjectTypes = props.selectOptionsTypes.filter(type => type.companyId == selectedCompany)
    const onChange = () =>  {
        props.setObjectTypeId(null);
        form.setValue('objectTypeId', null);
        console.log(form.watch('objectTypeId'), 'obj id');
    };
    
    const handleClose = () => {
        setOpenAlertGreen(false);
        setOpenAlertRed(false);
    };
    return (
        <div>
            <form className={classes.root} autoComplete="off">
                <MySelect required={ {message: '', value: true} }
                          selectOptions={props.selectOptionsСompanies}
                          form={form}
                          onChange={onChange}
                          name="companyId"
                          nameSwlect="Выберите компанию"
                />
                <MySelect required 
                          name="objectTypeId" 
                          selectOptions={availableObjectTypes} 
                          form={form}
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

            <Snackbar open={openAlertGreen}  autoHideDuration={6000} onClose={handleClose}>
                <div className="alert alert-success" role="alert">
                    Тип объектов успешно создан!
                    <button type="button" className="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </Snackbar>

            <Snackbar open={openAlertRed} autoHideDuration={6000} onClose={handleClose}>
                <div className="alert alert-danger" role="alert">
                    Произошла ошибка, тип объекта не создан!
                    <button type="button" className="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </Snackbar>
        </div>
    );
}