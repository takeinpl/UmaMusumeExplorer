using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmamusumeExplorer.Controls
{
    internal class MultiStateButton : Button
    {
        private readonly List<Enum> states = [];
        private readonly List<string> stateTexts = [];

        private int stateIndex = 0;

        public MultiStateButton()
        {
        }

        public void Setup(Type statesType, List<string>? stateTexts = null)
        {
            if (!statesType.IsEnum)
                throw new ArgumentException("Should be an enum.", nameof(statesType));

            states.Clear();

            Array values = Enum.GetValues(statesType);
            for (int i = 0; i < values.Length; i++)
            {
                if (values.GetValue(i) is Enum enumValue)
                    states.Add(enumValue);
            }

            if (stateTexts is null)
            {
                this.stateTexts.Clear();

                Array names = Enum.GetNames(statesType);
                for (int i = 0; i < values.Length; i++)
                {
                    if (names.GetValue(i) is string name)
                        this.stateTexts.Add(name);
                }
            }
            else
            {
                this.stateTexts.Clear();
                this.stateTexts.AddRange(stateTexts);
            }

            stateIndex = 0;
            UpdateState();
        }

        public Enum? State
        {
            get
            {
                if (stateIndex > states.Count)
                    return states[stateIndex];

                else return null;
            }
            set
            {
                if (value is not null)
                {
                    int newStateIndex = states.IndexOf(value);
                    if (newStateIndex >= 0)
                        stateIndex = newStateIndex;
                    UpdateState();
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (states.Count > 0)
            {
                stateIndex = (stateIndex + 1) % states.Count;

                //if (stateIndex >= states.Count)
                //    stateIndex = 0;

                UpdateState();
                StateChanged?.Invoke(this, new(states[stateIndex]));
            }
        }

        private void UpdateState()
        {
            if (states.Count < 1 || stateTexts.Count < 1)
                return;

            Text = stateTexts[stateIndex];
        }

        public event EventHandler<MultiStateButtonEventArgs>? StateChanged;
    }

    public class MultiStateButtonEventArgs
    {
        public MultiStateButtonEventArgs(Enum value)
        {
            Value = value;
        }

        public Enum Value { get; }
    }
}
