// 代码生成时间: 2025-10-16 02:21:20
using System;
using System.Windows;
using System.Windows.Controls;

namespace BusinessRuleEngineApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessRuleEngine engine;

        public MainWindow()
        {
            InitializeComponent();
            engine = new BusinessRuleEngine();
        }

        private void EvaluateRules_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = engine.EvaluateRules();
                ResultTextBox.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace BusinessRuleEngineApp
{
    /// <summary>
    /// Represents a business rule.
    /// </summary>
    public abstract class BusinessRule
    {
        public abstract bool Evaluate();
    }

    /// <summary>
    /// Implements a business rule engine that evaluates a set of rules.
    /// </summary>
    public class BusinessRuleEngine
    {
        private List<BusinessRule> rules;

        public BusinessRuleEngine()
        {
            rules = new List<BusinessRule>();
        }

        public void AddRule(BusinessRule rule)
        {
            rules.Add(rule);
        }

        public bool EvaluateRules()
        {
            foreach (var rule in rules)
            {
                if (!rule.Evaluate())
                {
                    return false;
                }
            }
            return true;
        }
    }
}

// Additional implementation of specific business rules can be added here.
// For example:

namespace BusinessRuleEngineApp
{
    /// <summary>
    /// A specific business rule that checks if the user is over 18.
    /// </summary>
    public class AgeOver18Rule : BusinessRule
    {
        private int age;

        public AgeOver18Rule(int age)
        {
            this.age = age;
        }

        public override bool Evaluate()
        {
            return age > 18;
        }
    }
}
