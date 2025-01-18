namespace EPYSLSAILORAPP.SystemServices
{

    public class ViewComponentServices
    {
        private readonly List<Func<object>> _scripts = new List<Func<object>>();

        public IEnumerable<Func<object>> Scripts
        {
            get
            {
                foreach (var script in _scripts)
                {
                    yield return script;
                }
            }
        }

        public void RegisterScript(Func<object> script)
        {
            _scripts.Add(script);
        }
    }

}
