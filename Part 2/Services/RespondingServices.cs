using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Part_2.Services;

public class RespondingServices
{
    private string currentTopic = null;
    private Dictionary<string, List<string>> topicDetails = new Dictionary<string, List<string>>();

    public string GetRespond(string respond)
    {
        // Check for "tell me more" request FIRST
        if (respond.Contains("tell me more", StringComparison.OrdinalIgnoreCase))
        {
            if (!string.IsNullOrEmpty(currentTopic))
            {
                return GetDetailedResponse(currentTopic);
            }
            else
            {
                return "You haven't asked about any security topics yet. What would you like to know about?";
            }
        }

        // Store the topic from the message
        string detectedTopic = StoreTopic(respond);
        if (!string.IsNullOrEmpty(detectedTopic))
        {
            currentTopic = detectedTopic;
        }

        string lowerRespond = respond.ToLower();

        // IMPORTANT: Check security topics FIRST (before greetings)
        if (lowerRespond.Contains("password"))
        {
            return Definitions(respond);
        }
        else if (lowerRespond.Contains("phishing"))
        {
            return Definitions(respond);
        }
        else if (lowerRespond.Contains("safe") || lowerRespond.Contains("browsing"))
        {
            return Definitions(respond);
        }
        // Check goodbye
        else if (lowerRespond.Contains("bye") || lowerRespond.Contains("goodbye"))
        {
            return "Goodbye! Enjoy the rest of your day! 👋";
        }
        // Check thanks
        else if (lowerRespond.Contains("thanks") || lowerRespond.Contains("thank you"))
        {
            return "You're welcome! It's my pleasure to help you. 😊";
        }
        // Check questions about bot capabilities
        else if (lowerRespond.Contains("what can i ask") || lowerRespond.Contains("ask about you") || lowerRespond.Contains("can i ask"))
        {
            return "Anything related to Cyber Security: password, phishing, and safe browsing.";
        }
        // Check responses to "how are you"
        else if (lowerRespond.Contains("am good") || lowerRespond.Contains("i'm good") || lowerRespond.Contains("yourself") || lowerRespond.Contains("you?"))
        {
            return "I'm also good too! Glad to hear that! 😊";
        }
        // Check greetings (LAST because "hi" is inside "phishing")
        else if (lowerRespond.Contains("hello") || lowerRespond.Contains("hy"))
        {
            return "Hello! Welcome to CyberBot, How are you doing?";
        }
        else if (lowerRespond.Contains("hi"))
        {
            return "Hi there! Welcome to CyberBot, How are you doing?";
        }
        else
        {
            return "Oops! I didn't understand that. Could you rephrase? Try asking about 'password', 'phishing', or 'safe browsing'.";
        }
    }

    public string StoreTopic(string message)
    {
        string[] keywords = { "password", "security", "phishing", "safe browsing", "browsing" };

        foreach (string keyword in keywords)
        {
            if (message.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                currentTopic = keyword;
                return keyword;
            }
        }
        return null;
    }

    private string GetDetailedResponse(string keyword)
    {
        switch (keyword.ToLower())
        {
            case "password":
                return "🔐 **Password Security Tips:**\n" +
                       "• Use 12+ characters with uppercase, lowercase, numbers, and symbols\n" +
                       "• Never reuse passwords across different sites\n" +
                       "• Use a password manager like Bitwarden or LastPass\n" +
                       "• Enable 2-factor authentication (2FA) whenever possible\n" +
                       "• Change passwords every 3-6 months for critical accounts\n\n" +
                       "Would you like to know more about password managers?";

            case "security":
                return "🛡️ **Security Best Practices:**\n" +
                       "• Keep all software and apps updated\n" +
                       "• Use a VPN on public Wi-Fi\n" +
                       "• Enable firewall protection\n" +
                       "• Be cautious of suspicious links and attachments\n" +
                       "• Regularly backup important data\n\n" +
                       "Shall I explain how to set up 2FA?";

            case "phishing":
                return "🎣 **How to Spot Phishing Attacks:**\n" +
                       "• Check sender's email address carefully\n" +
                       "• Hover over links before clicking to see the real URL\n" +
                       "• Look for poor grammar and spelling errors\n" +
                       "• Never share passwords or sensitive info via email\n" +
                       "• When in doubt, contact the company directly through official channels\n\n" +
                       "Want me to show you an example of a phishing email?";

            case "safe browsing":
            case "browsing":
                return "🌐 **Safe Browsing Tips:**\n" +
                       "• Only visit trusted websites (look for HTTPS 🔒)\n" +
                       "• Avoid downloading files from unknown sources\n" +
                       "• Keep your browser and extensions updated\n" +
                       "• Use ad-blockers to avoid malicious ads\n" +
                       "• Clear your browser cache and cookies regularly\n\n" +
                       "Would you like tips for secure online shopping?";

            default:
                return "I can provide more details on that topic. What specifically would you like to know?";
        }
    }

    public string Definitions(string respond)
    {
        string lowerInput = respond.ToLower();

        if (lowerInput.Contains("password"))
        {
            return "🔑 **Password Definition:** A password is a secret word or phrase used to verify identity and gain access to a system or account.\n\n" +
                   "**Strong Password Tips:**\n" +
                   "• Use at least 8-12 characters\n" +
                   "• Include uppercase, lowercase, numbers, and symbols\n" +
                   "• Avoid personal info like your name or birthdate\n" +
                   "• Never reuse passwords across different sites\n\n" +
                   "Say 'tell me more' for detailed password security tips!";
        }
        else if (lowerInput.Contains("phishing"))
        {
            return "🎣 **Phishing Definition:** Phishing is a cyberattack where attackers impersonate legitimate organizations to trick you into revealing sensitive information like passwords, credit card numbers, or OTPs.\n\n" +
                   "**How to Protect Yourself:**\n" +
                   "• Always check the sender's email address\n" +
                   "• Hover over links before clicking\n" +
                   "• Never share passwords or OTPs with anyone\n" +
                   "• Look for spelling and grammar errors\n\n" +
                   "Say 'tell me more' for detailed phishing prevention tips!";
        }
        else if (lowerInput.Contains("safe") || lowerInput.Contains("browsing"))
        {
            return "🌐 **Safe Browsing Definition:** Safe browsing means practicing secure habits while surfing the internet to protect yourself from cyber threats like malware, phishing, and data theft.\n\n" +
                   "**Safe Browsing Tips:**\n" +
                   "• Visit only trusted websites with HTTPS (🔒)\n" +
                   "• Avoid suspicious downloads and pop-ups\n" +
                   "• Keep your browser updated\n" +
                   "• Use antivirus and firewall protection\n\n" +
                   "Say 'tell me more' for detailed safe browsing tips!";
        }
        else
        {
            return "I'm sorry, I didn't understand that. Please ask about 'password', 'phishing', or 'safe browsing'.";
        }
    }

    public void ClearMemory()
    {
        currentTopic = null;
        topicDetails.Clear();
    }

    public string GetCurrentTopic()
    {
        return currentTopic ?? "No topic currently being discussed";
    }
}